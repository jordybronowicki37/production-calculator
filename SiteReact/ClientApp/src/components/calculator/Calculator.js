import "./Calculator.css";
import {Component} from "react";
import ReactFlow, {Background, Controls, MarkerType, MiniMap, ReactFlowProvider} from 'react-flow-renderer';
import {throwWarningNotification} from "../notification/NotificationThrower";
import {Popup} from "../popup/Popup";
import {CalculatorToolbar} from "./CalculatorToolbar";
import Store from "../../data/DataStore";
import {calculate, fetchWorksheet} from "../../data/WorksheetAPI";
import {ProductManager} from "../entities/products/ProductManager";
import {RecipeCreator} from "../entities/recipes/RecipeCreator";
import {RecipeManager} from "../entities/recipes/RecipeManager";
import {NodeSpawn} from "./nodes/NodeSpawn";
import {NodeProduction} from "./nodes/NodeProduction";
import {NodeEnd} from "./nodes/NodeEnd";
import {nodeCreateProduct, nodeCreateRecipe, nodeRemove} from "../../data/NodeAPI";
import {connectionCreate, connectionDelete} from "../../data/ConnectionAPI";

export class Calculator extends Component {
  defaultEdgeOptions = {type: 'default', markerEnd: {type: MarkerType.Arrow}, animated: true};
  defaultNodeStyle = {width:"min-content", padding:0, textAlign:"initial"};
  unsubscribe;
  
  constructor(props) {
    super(props);
    
    let store = Store.getState();
    
    this.state = {
      flowInstance: null,
      wrapperInstance: null,
      worksheetId: props.match.params.id,
      worksheetLoading: true,
      
      worksheetData: store.worksheet,
      nodes: store.nodes,
      connections: store.connections,
      products: store.products,
      recipes: store.recipes,
      
      calculatorState: "loading",
      
      popupProductManagerOpen:false,
      popupRecipeCreatorOpen:false,
      popupRecipeManagerOpen:false,
      popupNodeSpawnPreviewOpen:false,
      popupNodeProductionPreviewOpen:false,
      popupNodeEndPreviewOpen:false,
      
      tempPositionData:null,
    }
    fetchWorksheet(this.state.worksheetId).then(r => {
      if (r.calculationSucceeded) {
        this.setState({calculatorState: "success"});
      } else {
        this.setState({calculatorState: "warning"});
      }
    }).then(() => {
      this.setState({worksheetLoading:false});
    }).catch(() => {
      this.setState({calculatorState: "error"});
    });
    
    this.unsubscribe = Store.subscribe(() => {
      this.setState({calculatorState: "refresh"});
      let store = Store.getState();
      
      this.setState({
        worksheetData: store.worksheet,
        nodes: store.nodes,
        connections: store.connections,
        products: store.products,
        recipes: store.recipes,
      })
    });
  }

  render() {
    let title = this.state.worksheetData ? this.state.worksheetData.name : "";
    let message = this.state.worksheetData ? this.state.worksheetData.calculationError : "";
    let nodes = this.state.nodes.map(node => {
      const {body, type} = createNodeBody(node.type, node, this.state.products, this.state.recipes);
      let tempPos = this.state.tempPositionData
      let position = node.position;
      if (tempPos !== null && tempPos.id === node.id) position = tempPos.position;
      
      return {
        id: node.id.toString(),
        type,
        style: this.defaultNodeStyle,
        data: { label: body },
        position,
      };
    });
    let edges = this.state.connections.map(edge => {
      let id1 = edge.inputNodeId.toString();
      let id2 = edge.outputNodeId.toString();
      return {
        id: edge.id,
        source: id1,
        target: id2
      };
    });
    
    if (this.state.worksheetLoading) return <div></div>
    
    return (
      <div className="calculator">
        <div hidden={!this.state.worksheetLoading} className="loading-screen">
          <div>Loading</div>
          <div><i className='bx bx-loader-alt bx-spin'/></div>
        </div>
        
        <Popup
          hidden={!this.state.popupProductManagerOpen}
          onClose={() => this.setState({popupProductManagerOpen:false})}>
          <ProductManager products={this.state.products}/>
        </Popup>
        
        <Popup
          hidden={!this.state.popupRecipeCreatorOpen}
          onClose={() => this.setState({popupRecipeCreatorOpen:false})}>
          <RecipeCreator/>
        </Popup>
        
        <Popup
          hidden={!this.state.popupRecipeManagerOpen}
          onClose={() => this.setState({popupRecipeManagerOpen:false})}>
          <RecipeManager/>
        </Popup>
        
        <Popup
          hidden={!this.state.popupNodeSpawnPreviewOpen}
          onClose={() => this.setState({popupNodeSpawnPreviewOpen:false})}>
          <h3>Spawn node</h3>
          <div className="user-select-none border border-secondary float-start m-1"><NodeSpawn previewMode/></div>
          <p>The spawn node inputs products into the calculator. This could for example be a resource mine or some other kind of collector.
            Targets can be set on this node to limit the generation of the node.</p>
        </Popup>
        
        <Popup
          hidden={!this.state.popupNodeProductionPreviewOpen}
          onClose={() => this.setState({popupNodeProductionPreviewOpen:false})}>
          <h3>Production node</h3>
          <div className="user-select-none border border-secondary float-start m-1"><NodeProduction previewMode/></div>
          <p>In the production node a recipe can be set. The recipe is a way to convert one or multiple products into others.</p>
        </Popup>
        
        <Popup
          hidden={!this.state.popupNodeEndPreviewOpen}
          onClose={() => this.setState({popupNodeEndPreviewOpen:false})}>
          <h3>End node</h3>
          <div className="user-select-none border border-secondary float-start m-1"><NodeEnd previewMode/></div>
          <p>The end node is the output for products and can be used to set output targets or to see waste products.</p>
        </Popup>
        
        <div className="calculator-screen">
          <div className="top-bar">
            <h1>Calculator</h1>
            <div>Worksheet: {title}</div>
          </div>
          
          <div className="flow-chart-container">
            <CalculatorToolbar calculator={this}/>
            
            <ReactFlowProvider>
              <div className="flow-chart-wrapper flex-grow-1" ref={this.setReactFlowWrapper}>
                <div className="calculation-states">
                  <div hidden={message===""} className="calculation-states-label">{message}</div>
                  <div className="calculation-states-icon" onClick={() => this.calculateWorksheet()}>
                    <i hidden={this.state.calculatorState !== "success"} title="Calculation success" className='bx bx-check'></i>
                    <i hidden={this.state.calculatorState !== "warning"} title="Calculation was unsuccessful" className='bx bx-error' style={{color:"#F12C2C"}}></i>
                    <i hidden={this.state.calculatorState !== "error"} title="Calculation had error's" className='bx bx-error-circle' style={{color:"#F1B22C"}}></i>
                    <i hidden={this.state.calculatorState !== "loading"} title="Calculating..." className='bx bx-loader-alt bx-spin'></i>
                    <i hidden={this.state.calculatorState !== "refresh"} title="Recalculate" className='bx bx-refresh'></i>
                  </div>
                </div>
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
          </div>

          <div hidden className="test-nodes">
            <div onDragStart={(event) => this.onDragStart(event, "Production")} draggable><NodeProduction previewMode/></div>
            <div onDragStart={(event) => this.onDragStart(event, "Spawn")} draggable><NodeSpawn previewMode/></div>
            <div onDragStart={(event) => this.onDragStart(event, "End")} draggable><NodeEnd previewMode/></div>
          </div>
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
            this.setState({tempPositionData: {position:change.position, id:change.id}})
          } else {
            const {position, id} = this.state.tempPositionData;
            Store.dispatch({type:"node/change/position", payload: {position, id}});
            // TODO update end position on back-end
          }
          break;
        case "remove":
          nodeRemove(change.id);
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
  };
  onEdgesChange = (changes) => {
    changes.forEach(change => {
      switch (change.type) {
        case "remove":
          connectionDelete(change.id);
          break;
        case "select":
        case "add":
        case "reset":
        default:
          // console.log(`Non implemented edge change: ${change.type}`);
          // console.log(changes);
      }
    })
  };
  onConnect = edge => {
    let {products, nodes} = Store.getState();
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
    
    connectionCreate(edge.source, edge.target, product)
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
    
    this.onAddNode(nodetype, position)
  };
  onAddNode(nodetype, position) {
    const store = Store.getState();
    if (!position) position = {x:0,y:0};
    
    switch (nodetype) {
      case "Spawn":
      case "End":
        if (store.products.length === 0) {
          throwWarningNotification("Cannot create node because no products exist in worksheet");
          return;
        }
        let product = store.products[0];
        nodeCreateProduct(nodetype, position, product.name);
        break;
      case "Production":
        if (store.recipes.length === 0) {
          throwWarningNotification("Cannot create node because no recipes exist in worksheet");
          return;
        }
        let recipe = store.recipes[0];
        nodeCreateRecipe(nodetype, position, recipe.name);
        break;
    }
  }
  
  calculateWorksheet() {
    this.setState({calculatorState: "loading"});
    calculate().then(r => {
      if (r.calculationSucceeded) {
        this.setState({calculatorState: "success"});
      } else {
        this.setState({calculatorState: "warning"});
      }
    }).catch(r => {
      this.setState({calculatorState: "error"});
    });
  }
  
  componentWillUnmount() {
    this.unsubscribe();
  }
}

function createNodeBody(nodeType, data, products, recipes) {
  let body, type, product, recipe;
  switch (nodeType) {
    case "Spawn":
      product = findById(data.product, products);
      body = <NodeSpawn node={data} product={product} products={products}/>;
      type = "input";
      break;
    case "Production":
      recipe = findById(data.recipe, recipes)
      body = <NodeProduction node={data} recipe={recipe} products={products} recipes={recipes}/>;
      type = "default";
      break;
    case "End":
      product = findById(data.product, products)
      body = <NodeEnd node={data} product={product} products={products}/>;
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
