import './Node.css';
import './NodeSpawn.css';
import {nodeEditProduct} from "./NodeAPI";
import {TargetManager} from "../targets/TargetManager";
import {useState} from "react";

export function NodeSpawn({node, product, products, previewMode}) {
  const [editorOpen, setEditorOpen] = useState(false);
    
  let productField = <div className="preview-field">name</div>;
  let amountField = <div className="preview-field">0</div>
  let activeTargets = <div></div>;
  let targetEditor = <div></div>;

  if (!previewMode) {
    const {id, targets, amount} = node;
    productField =
      <select value={product.name} onChange={e => productChanged(id, e.target.value)}>
        {products.map(v => <option key={v.name} value={v.name}>{v.name}</option>)}
      </select>;
    let targetIcon = <div></div>;
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
    amountField = <div>{amount}</div>;
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
    <div className="node node-spawn">
      <div className="top-container">
        <h3>Spawn</h3>
        {activeTargets}
      </div>
      <div className="content-table">
        <div>Product</div>
        {productField}
        <div>Amount</div>
        {amountField}
      </div>
      {targetEditor}
    </div>
  );
}

function productChanged(id, name) {
  nodeEditProduct(id, name);
}
