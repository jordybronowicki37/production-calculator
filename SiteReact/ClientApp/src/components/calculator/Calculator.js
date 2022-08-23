import "./Calculator.css";
import {Component} from "react";
import {NodeProduction} from "../nodes/NodeProduction";
import {NodeSpawn} from "../nodes/NodeSpawn";
import {NodeEnd} from "../nodes/NodeEnd";
import ReactFlow, {MiniMap, Controls, Background, MarkerType, 
  ReactFlowProvider} from 'react-flow-renderer';
import {ProductManager} from "../products/ProductManager";
import {RecipeManager} from "../recipes/RecipeManager";
import Store from "../../dataStore/DataStore";
import {fetchWorksheet} from "../worksheets/WorksheetAPI";
import {nodeCreateProduct, nodeCreateRecipe} from "../nodes/NodeAPI";

export class Calculator extends Component {
  defaultEdgeOptions = {type: 'default', markerEnd: {type: MarkerType.Arrow}, animated: true};
  defaultNodeStyle = {width:"min-content", padding:0, textAlign:"initial"};
  unsubscribe;
  
  constructor(props) {
    super(props);
    this.state = {
      flowInstance: null,
      wrapperInstance: null,
      worksheetId: props.match.params.id,
    }
    fetchWorksheet(this.state.worksheetId);
    this.unsubscribe = Store.subscribe(() => this.forceUpdate());
  }

  render() {
    let state = Store.getState();
    let title = state.worksheet ? state.worksheet.name : "";
    let nodes = state.node.map(node => {
      const {body, type} = createNodeBody(node.type, node);
      return {
        id: node.id.toString(),
        type,
        style: this.defaultNodeStyle,
        data: { label: body },
        position: node.position,
      };
    });
    let edges = state.connection.map(edge => {
      let id1 = edge.inputNodeId.toString();
      let id2 = edge.outputNodeId.toString();
      return {
        id: `${id1}-${id2}`,
        source: id1,
        target: id2
      };
    });
    
    return (
      <div>
        <h1>Calculator</h1>
        <div>Worksheet: {title}</div>
        <div className="flow-chart-container">
          <ReactFlowProvider>
            <div ref={this.setReactFlowWrapper}>
              <ReactFlow 
                className="flow-chart"
                nodes={nodes}
                edges={edges}
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

  setReactFlowInstance = instance => this.setState({flowInstance: instance});
  setReactFlowWrapper = instance => this.setState({wrapperInstance: instance});
  
  onNodesChange = (changes) => {
    changes.forEach(change => {
      switch (change.type) {
        case "position":
          if (change.dragging) {
            Store.dispatch({type:"node/change/position", payload: {position:change.position, id:change.id}});
          } else {
            let position = Store.getState().node.find(value => value.id == change.id).position;
            // TODO update end position on back-end
          }
          break;
        case "dimensions":
        case "select":
        case "remove":
        case "add":
        case "reset":
        default:
          console.log(`Non implemented node change: ${change.type}`);
          console.log(change);
      }
    })
  };
  onEdgesChange = (changes) => {
    changes.forEach(change => {
      switch (change.type) {
        case "select":
        case "remove":
        case "add":
        case "reset":
        default:
          console.log(`Non implemented edge change: ${change.type}`);
          console.log(changes);
      }
    })
  };
  onConnect = edge => {
    console.log(edge);
  };
  
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
    const nodetype = event.dataTransfer.getData('application/reactflow');

    // check if the dropped element is valid
    if (typeof nodetype === 'undefined' || !nodetype) return;

    const position = this.state.flowInstance.project({
      x: event.clientX - reactFlowBounds.left,
      y: event.clientY - reactFlowBounds.top,
    });
    
    switch (nodetype) {
      case "Spawn":
      case "End":
        let product = Store.getState().product[0];
        nodeCreateProduct(this.state.worksheetId, nodetype, position, product.name);
        break;
      case "Production":
        let recipe = Store.getState().recipe[0];
        nodeCreateRecipe(this.state.worksheetId, nodetype, position, recipe.name);
        break;
    }
  };
  
  componentWillUnmount() {
    this.unsubscribe();
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