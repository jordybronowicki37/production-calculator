import {onDragStart} from "../Calculator";
import "./NodesSelector.css";

export function NodesSelector() {
  return (
    <div className="nodes-selector">
      <div className="item-spawn" title="Spawn-node" draggable
           onDragStart={(event) => onDragStart(event, "Spawn")}>
        <span className="material-symbols-rounded">exit_to_app</span>
      </div>
      <div className="item-production" title="Production-node" draggable
           onDragStart={(event) => onDragStart(event, "Production")}>
        <span className="material-symbols-rounded">factory</span>
      </div>
      <div className="item-end" title="End-node" draggable
           onDragStart={(event) => onDragStart(event, "End")}>
        <span className="material-symbols-rounded">output</span>
      </div>
    </div>
  )
}