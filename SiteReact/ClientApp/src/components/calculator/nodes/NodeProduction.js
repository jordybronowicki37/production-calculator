import './Node.css';
import './NodeProduction.css';
import {TargetManager} from "../targets/TargetManager";
import {useState} from "react";
import {nodeEditRecipe} from "../../../data/api/NodeAPI";
import {ActiveTargetsIcon} from "../targets/ActiveTargetsIcon";
import {NodeDragHandle} from "./NodeDragHandle";

export function NodeProduction({node, recipe, recipes, products, previewMode}) {
  const [editorOpen, setEditorOpen] = useState(false);

  let recipeField, amountField, productInList, productOutList, targetEditor;

  if (previewMode) {
    recipeField = <div className="preview-field">name</div>;
    amountField = <div className="preview-field">0</div>;
    productInList = <div className="node-list">
        <div>
          <div className="previewField">name</div>
          <div className="previewField">0</div>
        </div>
      </div>;
    productOutList = 
      <div className="node-list">
        <div>
          <div className="previewField">name</div>
          <div className="previewField">0</div>
        </div>
      </div>;
    targetEditor = <div></div>;
  } else {
    const {id, targets, amount} = node;

    recipeField = 
      <div>
        <select value={recipe.name} onChange={e => recipeChanged(id, e.target.value)}>
          <option value="" disabled hidden></option>
          {recipes.map(v => <option key={v.name} value={v.name}>{v.name}</option>)}
        </select>
      </div>;
    amountField = <div>{amount}</div>;
    productInList = generateProductList(recipe.inputThroughPuts, products);
    productOutList = generateProductList(recipe.outputThroughPuts, products);
    
    targetEditor =
      <div className="target-editor-wrapper" hidden={!editorOpen}>
        <button type="button" className="popup-close-button" onClick={() => setEditorOpen(false)}>
          <i className='bx bx-x'></i>
        </button>
        <TargetManager nodeId={id} targets={targets}></TargetManager>
      </div>
  }

  return (
    <div className="node node-production">
      <div className="top-container">
        <h3>Production</h3>
        <NodeDragHandle/>
        <ActiveTargetsIcon targets={node?node.targets:[]} onOpenEditor={() => setEditorOpen(true)}/>
      </div>
      <div className="content-container">
        <div className="content-table">
          <div>Recipe:</div>
          {recipeField}
          <div>Amount:</div>
          {amountField}
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

function generateProductList(throughputList, productList) {
  return (
    <div className="node-list">
      {throughputList.map((v, i) => {
        const product = findProduct(productList, v.productId);
        return (
          <div key={i}>
            <div className="recipe-product">{product.name}:</div>
            <div>{v.amount}</div>
          </div>
        )
      })}
    </div>
  );
}

function recipeChanged(id, name) {
  nodeEditRecipe(id, name);
}

function findProduct(products, id) {
  return products.find(v => v.id === id);
}
