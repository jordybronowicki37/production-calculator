import "./Calculator.scss";
import {useEffect, useState} from "react";
import ReactFlow, {Background, Controls, MarkerType, MiniMap, ReactFlowProvider} from 'react-flow-renderer';
import {throwWarningNotification} from "../notification/NotificationThrower";
import Store from "../../data/DataStore";
import {calculate} from "../../data/api/WorksheetAPI";
import {NodeSpawn} from "./nodes/NodeSpawn";
import {NodeProduction} from "./nodes/NodeProduction";
import {NodeEnd} from "./nodes/NodeEnd";
import {nodeEditPosition, nodeRemove} from "../../data/api/NodeAPI";
import {connectionCreate, connectionDelete} from "../../data/api/ConnectionAPI";
import {CalculationState} from "./CalculationState";
import {NodesSelector} from "./nodes/components/NodesSelector";
import {NodeOptionsEditorPopup} from "./nodes/components/NodeOptionsEditorPopup";

const defaultEdgeOptions = {type: 'default', markerEnd: {type: MarkerType.Arrow}, animated: true};
const defaultNodeStyle = {width:"min-content", padding:0, textAlign:"initial", border: "none", borderRadius: "5px", backgroundColor: "transparent"};

export function Calculator({worksheet, products, recipes, machines}){
  const { connections, nodes, calculationSucceeded, calculationError, id } = worksheet;
  
  const [calculationState, setCalculationState] = useState(calculationSucceeded ? "success" : "warning");
  const [nodeOptionsEditorOpen, setNodeOptionsEditorOpen] = useState(false);
  const [nodeEditorOptions, setNodeEditorOptions] = useState({mode:"create", nodeType:"End", position:{x:0,y:0}});
  
  const [reactFlowWrapper, setReactFlowWrapper] = useState(null);
  const [reactFlowInstance, setReactFlowInstance] = useState(null);
  const [tempPositionData, setTempPositionData] = useState(null);
  
  let message = calculationError;
  let flowNodes = generateNodes(id, nodes, products, recipes, machines, tempPositionData);
  let flowEdges = generateEdges(connections);

  const onCreateNewNode = (nodeType) => {
    setNodeOptionsEditorOpen(true);
    setNodeEditorOptions({mode:"create", nodeType:nodeType, position:{x:0,y:0}});
  };
  
  useEffect(() => {
    if (reactFlowInstance != null) {
      reactFlowInstance.fitView();
    }
  }, [reactFlowInstance]);
  
  return (
    <div className="calculator">
      <NodeOptionsEditorPopup 
        worksheetId={worksheet.id} 
        products={products} 
        recipes={recipes} 
        machines={machines} 
        options={nodeEditorOptions}
        hidden={!nodeOptionsEditorOpen} 
        onClose={() => setNodeOptionsEditorOpen(false)}/>
      
      <NodesSelector onCreateNewNode={onCreateNewNode} onDragStart={onDragStart}/>
      
      <ReactFlowProvider>
        <div className="flow-chart-wrapper flex-grow-1" ref={setReactFlowWrapper}>
          <CalculationState onClick={() => calculateWorksheet(id, setCalculationState)} message={message} state={calculationState}/>
          <ReactFlow
            className="flow-chart"
            nodes={flowNodes}
            edges={flowEdges}
            onNodesChange={(changes) => onNodesChange(id, changes, tempPositionData, setTempPositionData)}
            onEdgesChange={(changes) => onEdgesChange(id, changes)}
            onConnect={(edge) => onConnect(id, edge, nodes, products, recipes)}
            onDragOver={onDragOver}
            onInit={setReactFlowInstance}
            onDrop={(event) => {
              const { position, nodeType } = onDrop(event, reactFlowWrapper, reactFlowInstance);
              setNodeOptionsEditorOpen(true);
              setNodeEditorOptions({mode:"create", nodeType, position});
            }}
            defaultEdgeOptions={defaultEdgeOptions}>
            <MiniMap 
                nodeStrokeColor={(node) => {
                  switch (node.nodeType) {
                      case "Spawn":
                        return "var(--node-spawn-secondary)";
                      case "Production":
                        return "var(--node-production-secondary)";
                      case "End":
                        return "var(--node-end-secondary)";
                    default:
                      return "#fff";
                  }
                }}
                nodeColor="transparent"
                maskColor="#333"
                style={{backgroundColor:"#444"}}/>
            <Controls/>
            <Background color="#bbb" style={{backgroundColor:"#444"}}/>
          </ReactFlow>
        </div>
      </ReactFlowProvider>
    </div>
  );
}

function generateNodes(worksheetId, nodes, products, recipes, machines, tempPositionData) {
  return nodes.map((node) => {
    const {body, type} = createNodeBody(worksheetId, node.type, node, products, recipes, machines);
    let position = node.position;
    if (tempPositionData !== null && tempPositionData.id === node.id) position = tempPositionData.position;

    return {
      id: node.id.toString(),
      type,
      nodeType: node.type,
      style: defaultNodeStyle,
      data: {label: body},
      dragHandle: ".node-drag-handle",
      position,
    };
  });
}

function generateEdges(edges) {
  return edges.map(edge => {
    let id1 = edge.inputNodeId.toString();
    let id2 = edge.outputNodeId.toString();
    return {
      id: edge.id,
      source: id1,
      target: id2
    };
  });
}

function calculateWorksheet(worksheetId, setState) {
  setState("loading");
  calculate(worksheetId).then(r => {
    if (r.calculationSucceeded) {
      setState("success");
    } else {
      setState("warning");
    }
  }).catch(r => {
    setState("error");
  });
}

function createNodeBody(worksheetId, nodeType, data, products, recipes, machines) {
  let body, type, product, recipe, machine;
  switch (nodeType) {
    case "Spawn":
      product = findById(data.product, products);
      body = <NodeSpawn worksheetId={worksheetId} node={data} product={product}/>;
      type = "input";
      break;
    case "Production":
      recipe = findById(data.recipe, recipes);
      machine = findById(data.machine, machines);
      body = <NodeProduction worksheetId={worksheetId} node={data} machine={machine} recipe={recipe} products={products}/>;
      type = "default";
      break;
    case "End":
      product = findById(data.product, products)
      body = <NodeEnd worksheetId={worksheetId} node={data} product={product}/>;
      type = "output";
      break;
    default:
      type = "default";
      body = <div/>;
  }
  return {body, type};
}

function findById(id, list) {
  return list.find(v => v.id === id);
}

const onNodesChange = (worksheetId, changes, tempPositionData, setTempPositionData) => {
  changes.forEach(change => {
    switch (change.type) {
      case "position":
        if (change.dragging) {
          setTempPositionData({position:change.position, id:change.id});
        } else {
          Store.dispatch({type:"node/change/position", payload: tempPositionData, worksheetId:worksheetId});
          nodeEditPosition(worksheetId, change.id, tempPositionData.position);
        }
        break;
      case "remove":
        nodeRemove(worksheetId, change.id);
        break;
      case "dimensions":
      case "select":
      case "add":
      case "reset":
      default:
      // console.log(`Non implemented node change: ${change.type}`);
      // console.log(change);
    }
  })
}

function onEdgesChange (worksheetId, changes) {
  changes.forEach(change => {
    switch (change.type) {
      case "remove":
        connectionDelete(worksheetId, change.id);
        break;
      case "select":
      case "add":
      case "reset":
      default:
      // console.log(`Non implemented edge change: ${change.type}`);
      // console.log(changes);
    }
  })
}

function onConnect (worksheetId, edge, nodes, products, recipes) {
  if (products.length === 0) {
    throwWarningNotification("Cannot connect nodes because no products exist in worksheet");
    return;
  }
  let source = nodes.find(n => n.id === edge.source);
  let target = nodes.find(n => n.id === edge.target);
  let product;
  
  if (source.product != null) {
    product = source.product;
  } else if (target.product != null) {
    product = target.product;
  } else if (source.recipe != null && findById(source.recipe, recipes).outputThroughPuts.length > 0) {
    product = findById(source.recipe, recipes).outputThroughPuts[0].product;
  } else if (target.recipe != null && findById(target.recipe, recipes).inputThroughPuts.length > 0) {
    product = findById(target.recipe, recipes).inputThroughPuts[0].product;
  } else {
    product = products[0];
  }
  
  connectionCreate(worksheetId, edge.source, edge.target, product)
}

export function onDragStart (event, nodetype) {
  event.dataTransfer.setData('application/reactflow', nodetype);
  event.dataTransfer.effectAllowed = 'move';
}

function onDragOver (event) {
  event.preventDefault();
  event.dataTransfer.dropEffect = 'move';
}

function onDrop (event, wrapperInstance, flowInstance, worksheetId, products, recipes) {
  event.preventDefault();
  const reactFlowBounds = wrapperInstance.getBoundingClientRect();
  const nodeType = event.dataTransfer.getData('application/reactflow');
  
  // check if the dropped element is valid
  if (typeof nodeType === 'undefined' || !nodeType) return;

  const position = flowInstance.project({
    x: event.clientX - reactFlowBounds.left,
    y: event.clientY - reactFlowBounds.top,
  });
  
  return {position, nodeType}
}
