import "./Calculator.scss";
import React, {CSSProperties, DragEvent, ReactNode, useEffect, useRef, useState} from "react";
import ReactFlow, {
  Background,
  Connection as FlowConnection,
  Controls,
  DefaultEdgeOptions,
  Edge,
  EdgeChange,
  MarkerType,
  MiniMap,
  Node as FlowNode,
  NodeChange,
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
import {CalculationState} from "./CalculationState";
import {NodesSelector} from "./nodes/components/NodesSelector";
import {NodeEditor, NodeEditorOptions} from "./nodes/components/NodeEditor";
import {Connection, Machine, Node, NodePosition, NodeTypes, Product, Recipe, Worksheet} from "../../data/DataTypes";
import {ProductsPreviewEdge} from "./connections/ProductsPreviewEdge";
import {Popup} from "../popup/Popup";
import {ConnectionsEditor} from "./connections/ConnectionsEditor";
import {WorksheetDto} from "../../data/api/ApiDtoTypes";

const defaultEdgeOptions: DefaultEdgeOptions = {
  type: 'productsPreviewEdge',
  markerEnd: {type: MarkerType.Arrow},
  animated: true
};
const defaultNodeStyle: CSSProperties = {
  width: "min-content",
  padding: 0,
  textAlign: "initial",
  border: "none",
  borderRadius: "5px",
  backgroundColor: "transparent"
};
const customEdges = {productsPreviewEdge: ProductsPreviewEdge}

export type CalculatorProps = {
  worksheet: Worksheet,
  products: Product[],
  recipes: Recipe[],
  machines: Machine[],
}

export function Calculator({worksheet, products, recipes, machines}: CalculatorProps): React.JSX.Element {
  const {connections, nodes, alerts, id} = worksheet;

  const [connectionsEditorOpen, setConnectionsEditorOpen] = useState<boolean>(false);
  const [connectionsEditorEdgeId, setConnectionsEditorEdgeId] = useState<string>("");
  const [nodeEditorOpen, setNodeEditorOpen] = useState<boolean>(false);
  const [nodeEditorOptions, setNodeEditorOptions] = useState<NodeEditorOptions>({
    mode: "create",
    nodeType: "End",
    position: {x: 0, y: 0}
  });

  const reactFlowWrapper = useRef<HTMLDivElement>(null);
  const [reactFlowInstance, setReactFlowInstance] = useState<ReactFlowInstance | null>(null);
  const [tempPositionData, setTempPositionData] = useState<TempPositionData[]>([]);

  const flowNodes = generateNodes(id, nodes, products, recipes, machines, tempPositionData);
  const flowEdges = generateEdges(connections, products, (edgeId) => {
    setConnectionsEditorEdgeId(edgeId);
    setConnectionsEditorOpen(true);
  });

  const onCreateNewNode = (nodeType: NodeTypes) => {
    setNodeEditorOpen(true);
    setNodeEditorOptions({mode: "create", nodeType: nodeType, position: {x: 0, y: 0}});
  };

  useEffect(() => {
    if (reactFlowInstance != null) {
      reactFlowInstance.fitView();
    }
  }, [reactFlowInstance]);

  return (
    <div className="calculator">
      <Popup
        hidden={!connectionsEditorOpen}
        onClose={() => setConnectionsEditorOpen(false)}>
        <ConnectionsEditor worksheetId={id} products={products} connections={connections}
                           edgeId={connectionsEditorEdgeId}/>
      </Popup>

      <Popup
        hidden={!nodeEditorOpen}
        onClose={() => setNodeEditorOpen(false)}>
        <NodeEditor
          worksheetId={worksheet.id}
          products={products}
          recipes={recipes}
          machines={machines}
          options={nodeEditorOptions}
        />
      </Popup>

      <NodesSelector onCreateNewNode={onCreateNewNode} onDragStart={onDragStart}/>

      <ReactFlowProvider>
        <div className="flow-chart-wrapper flex-grow-1" ref={reactFlowWrapper}>
          <CalculationState<WorksheetDto> onClick={() => calculateWorksheet(id)} alerts={alerts}/>
          <ReactFlow
            className="flow-chart"
            nodes={flowNodes}
            edges={flowEdges}
            edgeTypes={customEdges}
            onNodesChange={(changes: NodeChange[]) => onNodesChange(id, changes, tempPositionData, setTempPositionData)}
            onEdgesChange={(changes: EdgeChange[]) => onEdgesChange(id, changes, connections)}
            onConnect={(edge: FlowConnection) => onConnect(id, edge, nodes, products)}
            onDragOver={onDragOver}
            onInit={setReactFlowInstance}
            onDrop={(event: DragEvent) => {
              if (reactFlowWrapper.current == undefined || reactFlowInstance == null) return;
              const onDropData = onDrop(event, reactFlowWrapper.current, reactFlowInstance);
              if (onDropData == undefined) return;
              const {position, nodeType} = onDropData;
              setNodeEditorOpen(true);
              setNodeEditorOptions({mode: "create", nodeType, position});
            }}
            defaultEdgeOptions={defaultEdgeOptions}>
            <MiniMap
              nodeStrokeColor={(node: FlowNode & { nodeType: string }) => {
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
              maskColor="#3338"
              style={{backgroundColor: "#444"}}/>
            <Controls/>
            <Background color="#bbb" style={{backgroundColor: "#444"}}/>
          </ReactFlow>
        </div>
      </ReactFlowProvider>
    </div>
  );
}

function generateNodes(worksheetId: string, nodes: Node[], products: Product[], recipes: Recipe[], machines: Machine[], tempPositionData: TempPositionData[]): FlowNode[] {
  return nodes.map((node) => {
    const {body, type} = createNodeBody(worksheetId, node.type, node, products, recipes, machines);
    let position = node.position;

    const relevantTempPositionData = tempPositionData.find(v => v.id === node.id);
    if (relevantTempPositionData !== undefined) position = relevantTempPositionData.position;

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

function generateEdges(connections: Connection[], products: Product[], onOpenEditor: (edgeId: string) => void): Edge[] {
  const combinedConnections: { [key: string]: Connection[] } = {};

  connections.forEach(value => {
    const endPointIds = `${value.inputNodeId};${value.outputNodeId}`;
    const combine = combinedConnections[endPointIds];
    if (combine) {
      combine.push(value);
    } else {
      combinedConnections[endPointIds] = [value];
    }
  });

  return Object.entries(combinedConnections).map(([key, value]) => {
    const id1 = key.split(";")[0];
    const id2 = key.split(";")[1];

    return {
      id: key,
      source: id1,
      target: id2,
      data: {products, connections: value, onOpenEditor}
    };
  });
}

function calculateWorksheet(worksheetId: string): Promise<WorksheetDto> {
  return calculate(worksheetId);
}

function createNodeBody(
  worksheetId: string,
  nodeType: NodeTypes,
  node: Node,
  products: Product[],
  recipes: Recipe[],
  machines: Machine[]
): { body: ReactNode, type: string } {
  let body: ReactNode;
  let type = "default";
  let product: Product | undefined;
  let recipe: Recipe | undefined;
  let machine: Machine | undefined;
  switch (nodeType) {
    case "Spawn":
      if (node.product == null) {
        console.error(`Node is missing product id. Node: ${node}`);
        break;
      }
      product = findById(node.product, products);
      if (product == undefined) {
        console.error("Product was not found.");
        break;
      }
      body = <NodeSpawn worksheetId={worksheetId} node={node} product={product} previewMode={false}/>;
      type = "input";
      break;
    case "Production":
      if (node.recipe == null) {
        console.error(`Node is missing recipe id. Node: ${node}`);
        break;
      }
      if (node.machine == null) {
        console.error(`Node is missing machine id. Node: ${node}`);
        break;
      }
      recipe = findById(node.recipe, recipes);
      if (recipe == undefined) {
        console.error("Recipe was not found.");
        break;
      }
      machine = findById(node.machine, machines);
      if (machine == undefined) {
        console.error("Machine was not found.");
        break;
      }
      body = <NodeProduction worksheetId={worksheetId} node={node} machine={machine} recipe={recipe} products={products}
                             previewMode={false}/>;
      break;
    case "End":
      if (node.product == null) {
        console.error(`Node is missing product id. Node: ${node}`);
        break;
      }
      product = findById(node.product, products);
      if (product == undefined) {
        console.error("Product was not found.");
        break;
      }
      body = <NodeEnd worksheetId={worksheetId} node={node} product={product} previewMode={false}/>;
      type = "output";
      break;
    default:
      body = <div/>;
  }
  return {body, type};
}

function findById<T extends { id: string }>(id: string, list: T[]): T | undefined {
  return list.find(v => v.id === id);
}

const onNodesChange = (worksheetId: string, changes: NodeChange[], tempPositionData: TempPositionData[], setTempPositionData: (value: (v: TempPositionData[]) => TempPositionData[]) => void): void => {
  changes.forEach(change => {
    switch (change.type) {
      case "position":
        // If user starts or is still dragging
        if (change.dragging) {
          const position = change.position;
          if (position == undefined) return;
          const relevantTempPositionIndex = tempPositionData.findIndex(v => v.id === change.id);

          // Detect if the user just started dragging
          if (relevantTempPositionIndex === -1) {
            setTempPositionData(actualTempPositions => [...actualTempPositions, {
              position: position,
              id: change.id
            }]);
          }
          // Update the temp position if the user is still dragging
          else {
            setTempPositionData(actualTempPositions =>
              actualTempPositions.map((v, i) =>
                i === relevantTempPositionIndex ? {...v, position: position} : v));
          }
        }
        // Save the position if the user stops dragging
        else {
          const relevantTempPositionData = tempPositionData.find(v => v.id === change.id);
          if (relevantTempPositionData !== undefined) {
            nodeEditPosition(worksheetId, change.id, relevantTempPositionData.position).then(() => {
              setTempPositionData(v => v.filter(c => c.id !== relevantTempPositionData.id));
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
        break;
    }
  })
}

function onEdgesChange(worksheetId: string, changes: EdgeChange[], connections: Connection[]) {
  changes.forEach(change => {
    switch (change.type) {
      case "remove": {
        const nodeInId = change.id.split(";")[0];
        const nodeOutId = change.id.split(";")[1];
        const connectionsToRemove = connections.filter(c => c.inputNodeId === nodeInId && c.outputNodeId === nodeOutId);
        connectionsToRemove.forEach(c => connectionDelete(worksheetId, c.id));
        break;
      }
      case "select":
      case "add":
      case "reset":
      default:
        // console.log(`Non implemented edge change: ${change.type}`);
        // console.log(changes);
        break;
    }
  })
}

function onConnect(worksheetId: string, edge: FlowConnection, nodes: Node[], products: Product[]): void {
  if (products.length === 0) {
    throwWarningNotification("Cannot connect nodes because no products exist in worksheet");
    return;
  }
  if (edge.source == null || edge.target == null) return;
  const source = nodes.find(n => n.id === edge.source);
  const target = nodes.find(n => n.id === edge.target);
  if (source == undefined || target == undefined) return;
  let product: string;

  if (source.product != null) {
    product = source.product;
  } else if (target.product != null) {
    product = target.product;
  } else {
    product = products[0].id;
  }

  connectionCreate(worksheetId, edge.source, edge.target, product);
}

function onDragStart(event: DragEvent, nodeType: NodeTypes): void {
  event.dataTransfer.setData('application/reactflow', nodeType);
  event.dataTransfer.effectAllowed = 'move';
}

function onDragOver(event: DragEvent) {
  event.preventDefault();
  event.dataTransfer.dropEffect = 'move';
}

function onDrop(event: DragEvent, wrapperInstance: HTMLDivElement, flowInstance: ReactFlowInstance): {
  position: NodePosition,
  nodeType: NodeTypes
} | undefined {
  event.preventDefault();
  const reactFlowBounds = wrapperInstance.getBoundingClientRect();
  const nodeType = event.dataTransfer.getData('application/reactflow') as NodeTypes;

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
