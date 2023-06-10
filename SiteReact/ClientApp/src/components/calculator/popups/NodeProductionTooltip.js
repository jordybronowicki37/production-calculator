import {Popup} from "../../popup/Popup";
import {NodeProduction} from "../nodes/NodeProduction";

export function NodeProductionTooltip({hidden, onClose}) {
  return <Popup
    hidden={hidden}
    onClose={onClose}>
    <h3>Production node</h3>
    <div className="user-select-none border border-secondary float-start m-1"><NodeProduction previewMode/></div>
    <p>In the production node a recipe can be set. The recipe is a way to convert one or multiple products into others.</p>
  </Popup>
}