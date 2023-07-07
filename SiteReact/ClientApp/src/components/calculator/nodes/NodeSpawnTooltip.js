import {Popup} from "../../popup/Popup";
import {NodeSpawn} from "../nodes/NodeSpawn";

export function NodeSpawnTooltip({hidden, onClose}) {
  return <Popup
    hidden={hidden}
    onClose={onClose}>
    <h3>Spawn node</h3>
    <div className="user-select-none border border-secondary float-start m-1"><NodeSpawn previewMode/></div>
    <p>The spawn node inputs products into the calculator. This could for example be a resource mine or some other kind of collector.
      Targets can be set on this node to limit the generation of the node.</p>
  </Popup>
}