import {Node} from "./Node";
import './NodeProduction.css';
import {nodeEditRecipe} from "./NodeAPI";
import Store from "../../dataStore/DataStore";
import {TargetManager} from "../targets/TargetManager";

export class NodeProduction extends Node {
  constructor(props) {
    super(props);
  }
  
  render () {
    let generateProductList = (list) => {
      return (
        <div className="node-list">
          {list.map((value, index) => 
            <div key={index}>
              <div className="recipe-product">{value.product}: </div>
              <div>{value.amount}</div>
            </div>)}
        </div>
      );
    };
    
    let recipeField, amountField, productInList, productOutList, targets, targetEditor;
    
    if (super.previewMode()) {
      recipeField = <div className="preview-field">name</div>;
      amountField = <div className="preview-field">0</div>;
      productInList = <div className="node-list"><div>
          <div className="previewField">name</div>
          <div className="previewField">0</div>
        </div></div>;
      productOutList = <div className="node-list"><div>
          <div className="previewField">name</div>
          <div className="previewField">0</div>
        </div></div>;
      targets = <div></div>;
      targetEditor = <div></div>;
    } else {
      recipeField = <div>
        <select value={super.recipe()} onChange={e => this.RecipeChanged(e.target.value)}>
          <option value="" disabled hidden></option>
          {super.recipes().map(v => <option key={v.name} value={v.name}>{v.name}</option>)}
        </select></div>;
      amountField = <div>{super.amount()}</div>;
      productInList = generateProductList(super.requiredInProducts());
      productOutList = generateProductList(super.requiredOutProducts());
      let targetIcon;
      let targetData = this.state.data.targets;
      if (targetData.length !== 0) {
        if (targetData[0].type === "ExactAmount") {
          targetIcon = <div>
            <i className='bx bx-arrow-to-right right-one'></i>
            <i className='bx bx-arrow-to-left left-one'></i>
          </div>;
        } else if (targetData[0].type === "MinAmount" || targetData[0].type === "MaxAmount") {
          targetIcon = <div>
            <i className='bx bx-arrow-to-left right-one'></i>
            <i className='bx bx-arrow-to-right left-one'></i>
          </div>;
        }
      }
      targets =
        <div className="targets" onClick={e => this.setState({targetEditorOpen: true})}>
          {targetIcon}
          <i className='bx bx-target-lock'></i>
        </div>;
      targetEditor = 
        <div className="target-editor-wrapper" hidden={!this.state.targetEditorOpen}>
          <button type="button" className="popup-close-button" onClick={() => this.setState({targetEditorOpen: false})}>
            <i className='bx bx-x'></i>
          </button>
          <TargetManager nodeId={this.state.data.id} targets={targetData}></TargetManager>
        </div>
    }
    
    return (
      <div className="node node-production">
        <div className="top-container">
          <h3>Production</h3>
          {targets}
        </div>
        <div className="product-table">
          <div>Recipe: </div>
          {recipeField}
          <div>Amount: </div>
          {amountField}
          
          <div className="content-table">
            <div>Required in</div>
            <div>Required out</div>
            {productInList}
            {productOutList}
          </div>
        </div>
        {targetEditor}
      </div>
    );
  }

  RecipeChanged(name) {
    nodeEditRecipe(this.state.data.id, name);
  }

  componentWillUnmount() {
    this.unsubscribe();
  }
}