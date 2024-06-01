import './Node.scss';
import './NodeSpawn.scss';
import {TargetManager} from "../targets/TargetManager";
import {useState} from "react";
import {ActiveTargetsIcon} from "../targets/ActiveTargetsIcon";
import {NodeDragHandle} from "./components/NodeDragHandle";
import {RoundedAmountField} from "../../misc/RoundedAmountField";
import {Node, Product, Worksheet} from "../../../data/DataTypes";
import {useSelector} from "react-redux";
import {StoreStates} from "../../../data/DataStore";
import {WarningAlertIcon} from "../alerts/WarningAlertIcon";
import {ErrorAlertIcon} from "../alerts/ErrorAlertIcon";

export type NodeSpawnProps = {
  worksheetId: string, 
  node: Node, 
  product: Product, 
  previewMode: boolean
}

export function NodeSpawn({worksheetId, node, product, previewMode}: NodeSpawnProps) {
  const [editorOpen, setEditorOpen] = useState(false);
  const worksheets = useSelector<StoreStates, Worksheet[]>(state => state.worksheets);
  const alerts = worksheets.find(w => w.id === worksheetId)!.alerts.filter(a => a.nodeId === node.id);
    
  let productField = <div className="preview-field">name</div>;
  let amountField = <div className="preview-field">0</div>
  let targetEditor = <div></div>;

  if (!previewMode) {
    const {id, targets, amount} = node;
    productField = <div>{product.name}</div>;
    amountField = <RoundedAmountField amount={amount}/>;
    targetEditor =
      <div className="target-editor-wrapper" hidden={!editorOpen}>
        <button type="button" className="popup-close-button" onClick={() => setEditorOpen(false)}>
          <i className='bx bx-x'></i>
        </button>
        <TargetManager worksheetId={worksheetId} nodeId={id} targets={targets}></TargetManager>
      </div>
  }

  return (
    <div className="node node-spawn">
      <div className="top-container">
        <h3>Spawn</h3>
        <NodeDragHandle/>
        <ActiveTargetsIcon targets={node?node.targets:[]} onOpenEditor={() => setEditorOpen(true)}/>
        <WarningAlertIcon alerts={alerts} onOpenEditor={() => {}} />
        <ErrorAlertIcon alerts={alerts} onOpenEditor={() => {}} />
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
