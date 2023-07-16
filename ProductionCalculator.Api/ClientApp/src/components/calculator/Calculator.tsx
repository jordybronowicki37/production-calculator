import "./Calculator.scss";
import {CSSProperties, useEffect, useRef, useState} from "react";
import ReactFlow, {
  Background,
  Connection as FlowConnection,
  Controls,
  DefaultEdgeOptions, Edge, EdgeChange,
  MarkerType,
  MiniMap,
  NodeChange,
  Node as FlowNode,
  ReactFlowInstance,
  ReactFlowProvider
} from 'reactflow';
import 'reactflow/dist/style.css';
import {throwWarningNotification} from "../notification/NotificationThrower";
import {calculate} from "../../data/api/WorksheetAPI";
import {NodeSpawn} from "./nodes/NodeSpawn";
import {NodeProduction} from "./nodes/NodeProduction";
import {NodeEnd} from "./nodes/NodeEnd";
import {nodeEditPosition, nodeRemove} from "../../data/api/NodeAPI";
import {connectionCreate, connectionDelete} from "../../data/api/ConnectionAPI";
import {CalculationState, CalculationStateType} from "./CalculationState";
import {NodesSelector} from "./nodes/components/NodesSelector";
import {NodeEditorOptions, NodeOptionsEditorPopup} from "./nodes/components/NodeOptionsEditorPopup";
import {Connection, Machine, Node, NodePosition, NodeTypes, Product, Recipe, Worksheet} from "../../data/DataTypes";
import {NodeChange} from "react-flow-renderer/dist/esm/types/changes";

const defaultEdgeOptions: DefaultEdgeOptions = {type: 'default', markerEnd: {type: MarkerType.Arrow}, animated: true};
const defaultNodeStyle: CSSProperties = {width:"min-content", padding:0, textAlign:"initial", border: "none", borderRadius: "5px", backgroundColor: "transparent"};

export function Calculator({worksheet, products, recipes, machines}: {worksheet: Worksheet, products: Product[], recipes: Recipe[], machines: Machine[]}){
  const { connections, nodes, calculationSucceeded, calculationError, id } = worksheet;
  
  const [calculationState, setCalculationState] = useState<CalculationStateType>(calculationSucceeded ? "success" : "warning");
  const [nodeOptionsEditorOpen, setNodeOptionsEditorOpen] = useState<boolean>(false);
  const [nodeEditorOptions, setNodeEditorOptions] = useState<NodeEditorOptions>({mode:"create", nodeType:"End", position:{x:0,y:0}});
  
  const reactFlowWrapper = useRef<HTMLDivElement>();
  const [reactFlowInstance, setReactFlowInstance] = useState<ReactFlowInstance | null>(null);
  const [tempPositionData, setTempPositionData] = useState<TempPositionData | null>(null);
  
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
        <div className="flow-chart-wrapper flex-grow-1" ref={reactFlowWrapper}>
          <CalculationState onClick={() => calculateWorksheet(id, setCalculationState)} message={message} state={calculationState}/>
          <ReactFlow
            className="flow-chart"
            nodes={flowNodes}
            edges={flowEdges}
            edgeTypes={customEdges}
            onNodesChange={(changes) => onNodesChange(id, changes, tempPositionData, setTempPositionData)}
            onEdgesChange={(changes) => onEdgesChange(id, changes)}
            onConnect={(edge) => onConnect(id, edge, nodes, products, recipes)}
            onDragOver={onDragOver}
            onInit={setReactFlowInstance}
            onDrop={(event) => {
              const { position, nodeType } = onDrop(event, reactFlowWrapper.current, reactFlowInstance);
              setNodeOptionsEditorOpen(true);
              setNodeEditorOptions({mode:"create", nodeType, position});
            }}
            defaultEdgeOptions={defaultEdgeOptions}>
            <MiniMap 
                nodeStrokeColor={(node) => {
                  // @ts-ignore
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

function generateNodes(worksheetId: string, nodes: Node[], products: Product[], recipes: Recipe[], machines: Machine[], tempPositionData: TempPositionData): FlowNode[] {
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

function generateEdges(connections: Connection[]): Edge[] {
  return connections.map(connection => {
    let id1 = connection.inputNodeId.toString();
    let id2 = connection.outputNodeId.toString();
    return {
      id: connection.id,
      source: id1,
      target: id2
    };
  });
}

function calculateWorksheet(worksheetId: string, setState: (state: CalculationStateType) => void): void {
  setState("loading");
  calculate(worksheetId).then(r => {
    if (r.calculationSucceeded) {
      setState("success");
    } else {
      setState("warning");
    }
  }).catch(() => {
    setState("error");
  });
}

function createNodeBody(worksheetId: string, nodeType: NodeTypes, node: Node, products: Product[], recipes: Recipe[], machines: Machine[]): { body: Element, type: string } {
  let body, type, product, recipe, machine;
  switch (nodeType) {
    case "Spawn":
      product = findById(node.product, products);
      body = <NodeSpawn worksheetId={worksheetId} node={node} product={product} previewMode={false}/>;
      type = "input";
      break;
    case "Production":
      recipe = findById(node.recipe, recipes);
      machine = findById(node.machine, machines);
      body = <NodeProduction worksheetId={worksheetId} node={node} machine={machine} recipe={recipe} products={products} previewMode={false}/>;
      type = "default";
      break;
    case "End":
      product = findById(node.product, products)
      body = <NodeEnd worksheetId={worksheetId} node={node} product={product} previewMode={false}/>;
      type = "output";
      break;
    default:
      type = "default";
      body = <div/>;
  }
  return {body, type};
}

function findById(id: string, list: {id:string, [key: string]: any}[]) {
  return list.find(v => v.id === id);
}

const onNodesChange = (worksheetId: string, changes: NodeChange[], tempPositionData: TempPositionData | null, setTempPositionData: (value: TempPositionData | null) => void) => {
  changes.forEach(change => {
    switch (change.type) {
      case "position":
        if (change.dragging) {
          setTempPositionData({position:change.position, id:change.id});
        } else {
          // TODO Fix temp position data warping when promise is not finished yet
          if(tempPositionData != null) {
            nodeEditPosition(worksheetId, change.id, tempPositionData.position).then(() => {
              setTempPositionData(null);
            });
          }
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

function onEdgesChange (worksheetId: string, changes: EdgeChange[]) {
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

function onConnect (worksheetId: string, edge: FlowConnection, nodes: Node[], products: Product[], recipes: Recipe[]) {
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

export function onDragStart (event, nodeType: NodeTypes) {
  event.dataTransfer.setData('application/reactflow', nodeType);
  event.dataTransfer.effectAllowed = 'move';
}

function onDragOver (event) {
  event.preventDefault();
  event.dataTransfer.dropEffect = 'move';
}

function onDrop (event, wrapperInstance: HTMLDivElement, flowInstance: ReactFlowInstance): {position: NodePosition, nodeType: NodeTypes} {
  event.preventDefault();
  const reactFlowBounds = wrapperInstance.getBoundingClientRect();
  const nodeType = event.dataTransfer.getData('application/reactflow');
  
  // check if the dropped element is valid
  if (typeof nodeType === 'undefined' || !nodeType) return;

  const position = flowInstance.project({
    x: event.clientX - reactFlowBounds.left,
    y: event.clientY - reactFlowBounds.top,
  });
  
  return {position, nodeType};
}

type TempPositionData = {
  id: string,
  position: NodePosition,
}
