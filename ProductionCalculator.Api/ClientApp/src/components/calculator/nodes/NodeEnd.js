import './Node.scss';
import './NodeEnd.scss';
import {TargetManager} from "../targets/TargetManager";
import {useState} from "react";
import {ActiveTargetsIcon} from "../targets/ActiveTargetsIcon";
import {NodeDragHandle} from "./components/NodeDragHandle";
import {NodeAmountField} from "./components/NodeAmountField.tsx";

export function NodeEnd({worksheetId, node, product, previewMode}) {
  const [editorOpen, setEditorOpen] = useState(false);

  let productField = <div className="preview-field">name</div>;
  let amountField = <div className="preview-field">0</div>
  let targetEditor = <div></div>;

  if (!previewMode) {
    const {id, targets, amount} = node;
    productField = <div>{product.name}</div>;
    amountField = <NodeAmountField amount={amount}/>;
    targetEditor =
      <div className="target-editor-wrapper" hidden={!editorOpen}>
        <button type="button" className="popup-close-button" onClick={() => setEditorOpen(false)}>
          <i className='bx bx-x'></i>
        </button>
        <TargetManager worksheetId={worksheetId} nodeId={id} targets={targets}></TargetManager>
      </div>
  }

  return (
    <div className="node node-end">
      <div className="top-container">
        <h3>End</h3>
        <NodeDragHandle/>
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
