import './Node.css';
import './NodeProduction.css';
import {nodeEditRecipe} from "./NodeAPI";
import {TargetManager} from "../targets/TargetManager";
import {useState} from "react";

export function NodeProduction({node, recipe, recipes, products, previewMode}) {
  const [editorOpen, setEditorOpen] = useState(false);

  let recipeField, amountField, productInList, productOutList, activeTargets, targetEditor;

  if (previewMode) {
    recipeField = <div className="preview-field">name</div>;
    amountField = <div className="preview-field">0</div>;
    productInList = <div className="node-list">
      <div>
        <div className="previewField">name</div>
        <div className="previewField">0</div>
      </div>
    </div>;
    productOutList = <div className="node-list">
      <div>
        <div className="previewField">name</div>
        <div className="previewField">0</div>
      </div>
    </div>;
    activeTargets = <div></div>;
    targetEditor = <div></div>;
  } else {
    const {id, targets, amount} = node;

    recipeField = <div>
      <select value={recipe.name} onChange={e => recipeChanged(id, e.target.value)}>
        <option value="" disabled hidden></option>
        {recipes.map(v => <option key={v.name} value={v.name}>{v.name}</option>)}
      </select></div>;
    amountField = <div>{amount}</div>;
    productInList = generateProductList(recipe.inputThroughPuts, products);
    productOutList = generateProductList(recipe.outputThroughPuts, products);
    let targetIcon;
    if (targets.length !== 0) {
      if (targets[0].type === "ExactAmount") {
        targetIcon = <div>
          <i className='bx bx-arrow-to-right right-one'></i>
          <i className='bx bx-arrow-to-left left-one'></i>
        </div>;
      } else if (targets[0].type === "MinAmount" || targets[0].type === "MaxAmount") {
        targetIcon = <div>
          <i className='bx bx-arrow-to-left right-one'></i>
          <i className='bx bx-arrow-to-right left-one'></i>
        </div>;
      }
    }
    activeTargets =
      <div className="targets" onClick={() => setEditorOpen(true)}>
        {targetIcon}
        <i className='bx bx-target-lock'></i>
      </div>;
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
        {activeTargets}
      </div>
      <div className="product-table">
        <div>Recipe:</div>
        {recipeField}
        <div>Amount:</div>
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
