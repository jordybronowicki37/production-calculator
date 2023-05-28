import "./Calculator.css";
import {useState} from "react";
import ReactFlow, {Background, Controls, MarkerType, MiniMap, ReactFlowProvider} from 'react-flow-renderer';
import {throwWarningNotification} from "../notification/NotificationThrower";
import Store from "../../data/DataStore";
import {calculate} from "../../data/api/WorksheetAPI";
import {NodeSpawn} from "./nodes/NodeSpawn";
import {NodeProduction} from "./nodes/NodeProduction";
import {NodeEnd} from "./nodes/NodeEnd";
import {nodeCreateProduct, nodeCreateRecipe, nodeRemove} from "../../data/api/NodeAPI";
import {connectionCreate, connectionDelete} from "../../data/api/ConnectionAPI";
import {CalculationState} from "./CalculationState";
import {NodesSelector} from "./nodes/NodesSelector";

const defaultEdgeOptions = {type: 'default', markerEnd: {type: MarkerType.Arrow}, animated: true};
const defaultNodeStyle = {width:"min-content", padding:0, textAlign:"initial", border: "none", borderRadius: "5px", backgroundColor: "transparent"};

export function Calculator({worksheet, products, recipes}){
  const { connections, nodes, calculationSucceeded, calculationError, id } = worksheet;
  
  const [calculationState, setCalculationState] = useState(calculationSucceeded ? "success" : "warning")
  
  const [reactFlowWrapper, setReactFlowWrapper] = useState(null);
  const [reactFlowInstance, setReactFlowInstance] = useState(null);
  const [tempPositionData, setTempPositionData] = useState(null);
  
  let message = calculationError;
  let flowNodes = generateNodes(id, nodes, products, recipes, tempPositionData);
  let flowEdges = generateEdges(connections)
  debugger
  return (
    <div className="calculator">
      <NodesSelector/>
      <ReactFlowProvider>
        <div className="flow-chart-wrapper flex-grow-1" ref={setReactFlowWrapper}>
          <CalculationState onClick={() => calculateWorksheet(id, setCalculationState)} message={message} state={calculationState}/>
          <ReactFlow
            className="flow-chart"
            nodes={flowNodes}
            edges={flowEdges}
            onNodesChange={(changes) => onNodesChange(id, changes, tempPositionData, setTempPositionData)}
            onEdgesChange={(changes) => onEdgesChange(id, changes)}
            onConnect={(edge) => onConnect(id, edge, nodes, products)}
            onDragOver={onDragOver}
            onInit={setReactFlowInstance}
            onDrop={(event) => onDrop(event, reactFlowWrapper, reactFlowInstance, id, products, recipes)}
            defaultEdgeOptions={defaultEdgeOptions}>
            <MiniMap nodeStrokeColor="#fff" nodeColor="transparent" maskColor="#333" style={{backgroundColor:"#444"}}/>
            <Controls/>
            <Background color="#bbb" style={{backgroundColor:"#444"}}/>
          </ReactFlow>
        </div>
      </ReactFlowProvider>
    </div>
  );
}

function generateNodes(worksheetId, nodes, products, recipes, tempPositionData) {
  return nodes.map((node, i) => {
    const {body, type} = createNodeBody(worksheetId, node.type, node, products, recipes);
    let position = node.position;
    if (tempPositionData !== null && tempPositionData.id === node.id) position = tempPositionData.position;
    if (!position) position = {x:0, y:200*i};

    return {
      id: node.id.toString(),
      type,
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

function createNodeBody(worksheetId, nodeType, data, products, recipes) {
  let body, type, product, recipe;
  switch (nodeType) {
    case "Spawn":
      product = findById(data.product, products);
      body = <NodeSpawn worksheetId={worksheetId} node={data} product={product} products={products}/>;
      type = "input";
      break;
    case "Production":
      recipe = findById(data.recipe, recipes)
      body = <NodeProduction worksheetId={worksheetId} node={data} recipe={recipe} products={products} recipes={recipes}/>;
      type = "default";
      break;
    case "End":
      product = findById(data.product, products)
      body = <NodeEnd worksheetId={worksheetId} node={data} product={product} products={products}/>;
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
          // TODO update end position on back-end
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

function onConnect (worksheetId, edge, nodes, products) {
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
  } else if (source.recipe != null && source.recipe.outputThroughPuts.length > 0) {
    product = source.recipe.outputThroughPuts[0];
  } else if (target.recipe != null && target.recipe.inputThroughPuts.length > 0) {
    product = target.recipe.inputThroughPuts[0];
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
  const nodetype = event.dataTransfer.getData('application/reactflow');
  
  // check if the dropped element is valid
  if (typeof nodetype === 'undefined' || !nodetype) return;

  const position = flowInstance.project({
    x: event.clientX - reactFlowBounds.left,
    y: event.clientY - reactFlowBounds.top,
  });

  onAddNode(worksheetId, nodetype, position, products, recipes)
}

function onAddNode (worksheetId, nodeType, position, products, recipes) {
  if (!position) position = {x:0,y:0};

  switch (nodeType) {
    case "Spawn":
    case "End":
      if (products.length === 0) {
        throwWarningNotification("Cannot create node because no products exist in worksheet");
        return;
      }
      let product = products[0];
      nodeCreateProduct(worksheetId, nodeType, position, product.id);
      break;
    case "Production":
      if (recipes.length === 0) {
        throwWarningNotification("Cannot create node because no recipes exist in worksheet");
        return;
      }
      let recipe = recipes[0];
      nodeCreateRecipe(worksheetId, nodeType, position, recipe.id);
      break;
  }
}
