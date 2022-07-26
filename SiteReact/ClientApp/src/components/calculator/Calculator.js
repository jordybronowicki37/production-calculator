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
      worksheetId: "1",
      worksheetTitle: "",
    }
    this.fetchWorksheet();
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
          <div onDragStart={(event) => this.onDragStart(event, "Production")} draggable><NodeProduction/></div>
          <div onDragStart={(event) => this.onDragStart(event, "Spawn")} draggable><NodeSpawn/></div>
          <div onDragStart={(event) => this.onDragStart(event, "End")} draggable><NodeEnd/></div>
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
    let body;
    let type = "default";
    switch (nodetype) {
      case "Spawn":
        body = <NodeSpawn/>;
        type = "input";
        break;
      case "Production":
        body = <NodeProduction/>;
        type = "default";
        break;
      case "End":
        body = <NodeEnd/>;
        type = "output";
        break;
    }
    
    const newNode = {
      id: "temp"+this.tempId++,
      type,
      position,
      style: this.defaultNodeStyle,
      data: { label: body },
    }
    this.setNodes(this.state.nodes.concat(newNode));
  }
  
  fetchWorksheet() {
    fetch(`worksheet/${this.state.worksheetId}`).then(response => {
      response.json().then(worksheet => {
        console.log(worksheet);
        
        let nodes = worksheet.nodes.map((node, index) => {
          let body;
          let type = "default";
          switch (node.type) {
            case "Spawn":
              body = <NodeSpawn data={node}/>;
              type = "input";
              break;
            case "Production":
              body = <NodeProduction data={node}/>
              type = "default"
              break;
            case "End":
              body = <NodeEnd data={node}/>
              type = "output"
              break;
          }

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
      })
    })
  }
}