import "./Calculator.css";
import {Component} from "react";
import {NodeProduction} from "../nodes/NodeProduction";
import {NodeSpawn} from "../nodes/NodeSpawn";
import {NodeEnd} from "../nodes/NodeEnd";
import ReactFlow, {MiniMap, Controls, applyEdgeChanges, applyNodeChanges, addEdge, Background, MarkerType, 
  ReactFlowProvider} from 'react-flow-renderer';
import {ProductManager} from "../products/ProductManager";
import {RecipeManager} from "../recipes/RecipeManager";
import Store from "../../dataStore/DataStore";
import {fetchAllProducts} from "../products/ProductAPI";
import {fetchAllRecipes} from "../recipes/RecipeAPI";
import {fetchWorksheet} from "../worksheets/WorksheetAPI";

export class Calculator extends Component {
  defaultEdgeOptions = {type: 'default', markerEnd: {type: MarkerType.Arrow}, animated: true};
  defaultNodeStyle = {width:"min-content", padding:0, textAlign:"initial"};
  tempId = 0;
  
  constructor(props) {
    super(props);
    this.state = {
      nodes: [],
      edges: [],
      flowInstance: null,
      wrapperInstance: null,
      worksheetId: props.match.params.id,
      worksheetTitle: "",
    }
    this.loadWorksheet();
    fetchAllProducts(this.state.worksheetId);
    fetchAllRecipes(this.state.worksheetId);
  }

  render() {
    return (
      <div>
        <h1>Calculator</h1>
        <div>Worksheet: {this.state.worksheetTitle}</div>
        <div className="flow-chart-container">
          <ReactFlowProvider>
            <div ref={this.setReactFlowWrapper}>
              <ReactFlow 
                className="flow-chart"
                nodes={this.state.nodes}
                edges={this.state.edges}
                onNodesChange={this.onNodesChange}
                onEdgesChange={this.onEdgesChange}
                onConnect={this.onConnect}
                onDragOver={this.onDragOver}
                onInit={this.setReactFlowInstance}
                onDrop={this.onDrop}
                defaultEdgeOptions={this.defaultEdgeOptions}>
                <MiniMap/>
                <Controls/>
                <Background/>
              </ReactFlow>
            </div>
          </ReactFlowProvider>

          <div className="attribute-manager">
            <div className="product-manager">
              <h2>Products</h2>
              <ProductManager worksheetId={this.state.worksheetId}></ProductManager>
            </div>
            <div className="recipe-manager">
              <h2>Recipes</h2>
              <RecipeManager worksheetId={this.state.worksheetId}></RecipeManager>
            </div>
          </div>
        </div>
        
        <div className="test-nodes">
          <div onDragStart={(event) => this.onDragStart(event, "Production")} draggable><NodeProduction previewMode/></div>
          <div onDragStart={(event) => this.onDragStart(event, "Spawn")} draggable><NodeSpawn previewMode/></div>
          <div onDragStart={(event) => this.onDragStart(event, "End")} draggable><NodeEnd previewMode/></div>
        </div>
      </div>
    );
  }

  setNodes = nodes => this.setState({nodes: nodes});
  setEdges = edges => this.setState({edges:edges});
  setReactFlowInstance = instance => this.setState({flowInstance: instance});
  setReactFlowWrapper = instance => this.setState({wrapperInstance: instance});
  
  onNodesChange = (changes) => this.setNodes(applyNodeChanges(changes, this.state.nodes));
  onEdgesChange = (changes) => this.setEdges(applyEdgeChanges(changes, this.state.edges));
  onConnect = edge => this.setEdges(addEdge(edge, this.state.edges));
  
  onDragStart = (event, nodetype) => {
    event.dataTransfer.setData('application/reactflow', nodetype);
    event.dataTransfer.effectAllowed = 'move';
  };
  onDragOver = (event) => {
    event.preventDefault();
    event.dataTransfer.dropEffect = 'move';
  };
  onDrop = (event) => {
    event.preventDefault();
    const reactFlowBounds = this.state.wrapperInstance.getBoundingClientRect();
    const type = event.dataTransfer.getData('application/reactflow');

    // check if the dropped element is valid
    if (typeof type === 'undefined' || !type) return;

    const position = this.state.flowInstance.project({
      x: event.clientX - reactFlowBounds.left,
      y: event.clientY - reactFlowBounds.top,
    });
    this.addNewNode(type, position)
  };
  
  addNewNode(nodetype, position) {
    const {body, type} = createNodeBody(nodetype, undefined);
    const newNode = {
      id: "temp"+this.tempId++,
      type,
      position,
      style: this.defaultNodeStyle,
      data: { label: body },
    }
    this.setNodes(this.state.nodes.concat(newNode));
  }
  
  async loadWorksheet() {
    let worksheet = await fetchWorksheet(this.state.worksheetId);
    console.log(worksheet);
    
    let nodes = worksheet.nodes.map((node, index) => {
      const {body, type} = createNodeBody(node.type, node);
      return {
        id: node.id.toString(),
        type,
        style: this.defaultNodeStyle,
        data: { label: body },
        position: { x: 300, y: index*150 },
      };
    });
    
    let edges = worksheet.connections.map(edge => {
      let id1 = edge.inputNodeId.toString();
      let id2 = edge.outputNodeId.toString();
      return {
        id: `${id1}-${id2}`, 
        source: id1, 
        target: id2
      }
    })
    
    this.setState({
      nodes: nodes,
      edges: edges,
      worksheetTitle: worksheet.name,
    })
  }
}

function createNodeBody(nodeType, data) {
  let body, type;
  switch (nodeType) {
    case "Spawn":
      body = <NodeSpawn data={data}/>;
      type = "input";
      break;
    case "Production":
      body = <NodeProduction data={data}/>;
      type = "default";
      break;
    case "End":
      body = <NodeEnd data={data}/>;
      type = "output";
      break;
    default:
      body = <div></div>;
      type = "default";
  }
  return {body, type};
}