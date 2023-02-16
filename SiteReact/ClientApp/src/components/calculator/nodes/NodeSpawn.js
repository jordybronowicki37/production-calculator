import './Node.css';
import './NodeSpawn.css';
import {TargetManager} from "../targets/TargetManager";
import {useState} from "react";
import {nodeEditProduct} from "../../../data/NodeAPI";
import {ActiveTargetsIcon} from "../targets/ActiveTargetsIcon";

export function NodeSpawn({node, product, products, previewMode}) {
  const [editorOpen, setEditorOpen] = useState(false);
    
  let productField = <div className="preview-field">name</div>;
  let amountField = <div className="preview-field">0</div>
  let targetEditor = <div></div>;

  if (!previewMode) {
    const {id, targets, amount} = node;
    productField =
      <div>
        <select value={product.name} onChange={e => productChanged(id, e.target.value)}>
          {products.map(v => <option key={v.name} value={v.name}>{v.name}</option>)}
        </select>
      </div>;
    amountField = <div>{amount}</div>;
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
        <ActiveTargetsIcon targets={node?node.targets:[]} onOpenEditor={() => setEditorOpen(true)}/>
      </div>
      <div className="content-container">
        <div className="content-table">
          <div>Product</div>
          {productField}
          <div>Amount</div>
          {amountField}
        </div>
      </div>
      {targetEditor}
    </div>
  );
}

function productChanged(id, name) {
  nodeEditProduct(id, name);
}
