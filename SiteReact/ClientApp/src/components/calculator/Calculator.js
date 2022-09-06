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
import {calculate, fetchWorksheet} from "../worksheets/WorksheetAPI";
import {nodeCreateProduct, nodeCreateRecipe, nodeRemove} from "../nodes/NodeAPI";
import {connectionCreate, connectionDelete} from "../connections/ConnectionAPI";
import {RecipeCreator} from "../recipes/RecipeCreator";
import {Link} from "react-router-dom";

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
      worksheetLoading: true,
      
      calculatorStateSuccess:false,
      calculatorStateLoading:true,
      calculatorStateWarning:false,
      calculatorStateError:false,
      calculatorStateRefresh:false,
      
      dropMenuWorksheetOpen:false,
      dropMenuProductsOpen:false,
      dropMenuRecipesOpen:false,
      dropMenuNodesOpen:false,
      
      popupProductManagerOpen:false,
      popupRecipeCreatorOpen:false,
      popupRecipeManagerOpen:false,
      popupNodeSpawnPreviewOpen:false,
      popupNodeProductionPreviewOpen:false,
      popupNodeEndPreviewOpen:false,
    }
    fetchWorksheet(this.state.worksheetId).then(r => {
      this.setState({worksheetLoading:false});
      if (r.calculationSucceeded) {
        this.setCalculatorState("success");
      } else {
        this.setCalculatorState("warning");
      }
    }).catch(r => {
      this.setCalculatorState("error");
    });
    this.unsubscribe = Store.subscribe(() => {
      this.setCalculatorState("refresh");
      this.forceUpdate();
    });
  }

  render() {
    let state = Store.getState();
    let title = state.worksheet ? state.worksheet.name : "";
    let message = state.worksheet ? state.worksheet.calculationError : "";
    let nodes = state.nodes.map(node => {
      const {body, type} = createNodeBody(node.type, node);
      return {
        id: node.id.toString(),
        type,
        style: this.defaultNodeStyle,
        data: { label: body },
        position: node.position,
      };
    });
    let edges = state.connections.map(edge => {
      let id1 = edge.inputNodeId.toString();
      let id2 = edge.outputNodeId.toString();
      return {
        id: edge.id,
        source: id1,
        target: id2
      };
    });
    
    return (
      <div className="calculator-screen-manager">
        <div hidden={!this.state.worksheetLoading} className="loading-screen">
          <div>Loading</div>
          <div><i className='bx bx-loader-alt bx-spin'></i></div>
        </div>
        
        <div hidden={!this.state.popupProductManagerOpen} className="popup-container" onClick={() => this.setState({popupProductManagerOpen:false})}>
          <div onClick={e => e.stopPropagation()}>
            <button type="button" className="popup-close-button" onClick={() => this.setState({popupProductManagerOpen:false})}>
              <i className='bx bx-x'></i>
            </button>
            <ProductManager></ProductManager>
          </div>
        </div>
        
        <div hidden={!this.state.popupRecipeCreatorOpen} className="popup-container" onClick={() => this.setState({popupRecipeCreatorOpen:false})}>
          <div onClick={e => e.stopPropagation()}>
            <button type="button" className="popup-close-button" onClick={() => this.setState({popupRecipeCreatorOpen:false})}>
              <i className='bx bx-x'></i>
            </button>
            <RecipeCreator></RecipeCreator>
          </div>
        </div>
        <div hidden={!this.state.popupRecipeManagerOpen} className="popup-container" onClick={() => this.setState({popupRecipeManagerOpen:false})}>
          <div onClick={e => e.stopPropagation()}>
            <button type="button" className="popup-close-button" onClick={() => this.setState({popupRecipeManagerOpen:false})}>
              <i className='bx bx-x'></i>
            </button>
            <RecipeManager></RecipeManager>
          </div>
        </div>
        
        <div hidden={!this.state.popupNodeSpawnPreviewOpen} className="popup-container" onClick={() => this.setState({popupNodeSpawnPreviewOpen:false})}>
          <div onClick={e => e.stopPropagation()}>
            <button type="button" className="popup-close-button" onClick={() => this.setState({popupNodeSpawnPreviewOpen:false})}>
              <i className='bx bx-x'></i>
            </button>
            <div>
              <h3>Spawn node</h3>
              <div className="user-select-none border border-secondary float-start m-1"><NodeSpawn previewMode/></div>
              <p>The spawn node inputs products into the calculator. This could for example be a resource mine or some other kind of collector. 
                Targets can be set on this node to limit the generation of the node.</p>
            </div>
          </div>
        </div>
        <div hidden={!this.state.popupNodeProductionPreviewOpen} className="popup-container" onClick={() => this.setState({popupNodeProductionPreviewOpen:false})}>
          <div onClick={e => e.stopPropagation()}>
            <button type="button" className="popup-close-button" onClick={() => this.setState({popupNodeProductionPreviewOpen:false})}>
              <i className='bx bx-x'></i>
            </button>
            <div>
              <h3>Production node</h3>
              <div className="user-select-none border border-secondary float-start m-1"><NodeProduction previewMode/></div>
              <p>In the production node a recipe can be set. The recipe is a way to convert one or multiple products into others.</p>
            </div>
          </div>
        </div>
        <div hidden={!this.state.popupNodeEndPreviewOpen} className="popup-container" onClick={() => this.setState({popupNodeEndPreviewOpen:false})}>
          <div onClick={e => e.stopPropagation()}>
            <button type="button" className="popup-close-button" onClick={() => this.setState({popupNodeEndPreviewOpen:false})}>
              <i className='bx bx-x'></i>
            </button>
            <div>
              <h3>End node</h3>
              <div className="user-select-none border border-secondary float-start m-1"><NodeEnd previewMode/></div>
              <p>The end node is the output for products and can be used to set output targets or to see waste products.</p>
            </div>
          </div>
        </div>
        
        <div className="calculator-screen">
          <div className="top-bar">
            <h1>Calculator</h1>
            <div>Worksheet: {title}</div>
          </div>
          
          <div className="flow-chart-container">
            <div className="toolbar">
              <div>
                <button onClick={() => this.setDropDownState(this.state.dropMenuWorksheetOpen?"none":"worksheet")}
                       type="button" className={this.state.dropMenuWorksheetOpen?"selected":""}>Worksheet</button>
                <div className="drop-menu" hidden={!this.state.dropMenuWorksheetOpen}>
                  <Link to="/worksheets"><button type="button">View all</button></Link>
                  <button type="button">Change name</button>
                </div>
              </div>
              <div>
                <button onClick={() => this.setDropDownState(this.state.dropMenuProductsOpen?"none":"products")} 
                        type="button" className={this.state.dropMenuProductsOpen?"selected":""}>Products</button>
                <div className="drop-menu" hidden={!this.state.dropMenuProductsOpen}>
                  <button type="button" onClick={() => this.setState({popupProductManagerOpen:true})}>View products</button>
                  <button type="button">Add product</button>
                </div>
              </div>
              <div>
                <button onClick={() => this.setDropDownState(this.state.dropMenuRecipesOpen?"none":"recipes")} 
                        type="button" className={this.state.dropMenuRecipesOpen?"selected":""}>Recipes</button>
                <div className="drop-menu" hidden={!this.state.dropMenuRecipesOpen}>
                  <button type="button" onClick={() => this.setState({popupRecipeManagerOpen:true})}>View recipes</button>
                  <button type="button" onClick={() => this.setState({popupRecipeCreatorOpen:true})}>Add recipe</button>
                </div>
              </div>
              <div>
                <button onClick={() => this.setDropDownState(this.state.dropMenuNodesOpen?"none":"nodes")} 
                        className={this.state.dropMenuNodesOpen?"selected":""}>Nodes</button>
                <div className="drop-menu" hidden={!this.state.dropMenuNodesOpen}>
                  <div className="item-with-info">
                    <div className="draggable-node-icon flex-grow-1" onDragStart={(event) => this.onDragStart(event, "Spawn")} draggable>Spawn</div>
                    <i className='bx bx-info-circle' onClick={() => this.setState({popupNodeSpawnPreviewOpen:true})}></i>
                  </div>
                  <div className="item-with-info">
                    <div className="draggable-node-icon flex-grow-1" onDragStart={(event) => this.onDragStart(event, "Production")} draggable>Production</div>
                    <i className='bx bx-info-circle' onClick={() => this.setState({popupNodeProductionPreviewOpen:true})}></i>
                  </div>
                  <div className="item-with-info">
                    <div className="draggable-node-icon flex-grow-1" onDragStart={(event) => this.onDragStart(event, "End")} draggable>End</div>
                    <i className='bx bx-info-circle' onClick={() => this.setState({popupNodeEndPreviewOpen:true})}></i>
                  </div>
                </div>
              </div>
            </div>
            
            <ReactFlowProvider>
              <div className="flow-chart-wrapper flex-grow-1" ref={this.setReactFlowWrapper}>
                <div className="calculator-states">
                  <div hidden={message===""} className="calculator-states-label">{message}</div>
                  <div className="calculator-states-icon" onClick={() => this.calculateWorksheet()}>
                    <i hidden={!this.state.calculatorStateSuccess} title="Calculation success" className='bx bx-check'></i>
                    <i hidden={!this.state.calculatorStateWarning} title="Calculation was unsuccessful" className='bx bx-error' style={{color:"#F12C2C"}}></i>
                    <i hidden={!this.state.calculatorStateError} title="Calculation had error's" className='bx bx-error-circle' style={{color:"#F1B22C"}}></i>
                    <i hidden={!this.state.calculatorStateLoading} title="Calculating..." className='bx bx-loader-alt bx-spin'></i>
                    <i hidden={!this.state.calculatorStateRefresh} title="Recalculate" className='bx bx-refresh'></i>
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
            Store.dispatch({type:"node/change/position", payload: {position:change.position, id:parseInt(change.id)}});
          } else {
            let position = Store.getState().nodes.find(value => value.id === parseInt(change.id)).position;
            // TODO update end position on back-end
          }
          break;
        case "remove":
          nodeRemove(this.state.worksheetId, change.id);
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
          connectionDelete(this.state.worksheetId, change.id);
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
    let state = Store.getState().nodes;
    let source = state.find(n => n.id === parseInt(edge.source));
    let target = state.find(n => n.id === parseInt(edge.target));
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
      product = state.products[0];
    }
    
    connectionCreate(this.state.worksheetId, edge.source, edge.target, product.name)
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
        let product = Store.getState().products[0];
        nodeCreateProduct(this.state.worksheetId, nodetype, position, product.name);
        break;
      case "Production":
        let recipe = Store.getState().recipes[0];
        nodeCreateRecipe(this.state.worksheetId, nodetype, position, recipe.name);
        break;
    }
  };
  
  calculateWorksheet() {
    this.setCalculatorState("loading");
    calculate(this.state.worksheetId).then(r => {
      if (r.calculationSucceeded) {
        this.setCalculatorState("success");
      } else {
        this.setCalculatorState("warning");
      }
    }).catch(r => {
      this.setCalculatorState("error");
    });
  }
  
  setDropDownState(state) {
    let options = {
      dropMenuWorksheetOpen:false,
      dropMenuProductsOpen:false,
      dropMenuRecipesOpen:false,
      dropMenuNodesOpen:false,
    }
    
    switch (state) {
      case "worksheet":
        options.dropMenuWorksheetOpen = true;
        break;
      case "products":
        options.dropMenuProductsOpen = true;
        break;
      case "recipes":
        options.dropMenuRecipesOpen = true;
        break;
      case "nodes":
        options.dropMenuNodesOpen = true;
        break;
      case "none":
        break;
      default: return;
    }
    
    this.setState(options);
  }
  
  setCalculatorState(state) {
    let options = {
      calculatorStateSuccess:false,
      calculatorStateLoading:false,
      calculatorStateWarning:false,
      calculatorStateError:false,
      calculatorStateRefresh:false,
    }
    
    switch (state) {
      case "success":
        options.calculatorStateSuccess = true;
        break;
      case "loading":
        options.calculatorStateLoading = true;
        break;
      case "warning":
        options.calculatorStateWarning = true;
        break;
      case "error":
        options.calculatorStateError = true;
        break;
      case "refresh":
        options.calculatorStateRefresh = true;
        break;
      default:
        return;
    }
    
    this.setState(options);
  }
  
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