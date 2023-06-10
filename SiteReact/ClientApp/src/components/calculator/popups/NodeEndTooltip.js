import {Popup} from "../../popup/Popup";
import {NodeEnd} from "../nodes/NodeEnd";

export function NodeEndTooltip({hidden, onClose}) {
  return <Popup
    hidden={hidden}
    onClose={onClose}>
    <h3>End node</h3>
    <div className="user-select-none border border-secondary float-start m-1"><NodeEnd previewMode/></div>
    <p>The end node is the output for products and can be used to set output targets or to visualize the waste product amounts.</p>
  </Popup>
}