import "./NodesSelector.scss";
import {NodeTypes} from "../../../../data/DataTypes";
import {DragEvent} from "react";
import React from "react";

export type NodesSelectorProps = {
  onCreateNewNode: (type: NodeTypes) => void, 
  onDragStart: (event: DragEvent<HTMLDivElement>, type: NodeTypes) => void
}

export function NodesSelector({onCreateNewNode, onDragStart}: NodesSelectorProps): React.JSX.Element {
  return (
    <div className="nodes-selector">
      <div className="item-spawn" title="Spawn-node" draggable
           onClick={() => onCreateNewNode("Spawn")}
           onDragStart={(event) => onDragStart(event, "Spawn")}>
        <span className="material-symbols-rounded">pallet</span>
      </div>
      
      <div className="item-production" title="Production-node" draggable
           onClick={() => onCreateNewNode("Production")}
           onDragStart={(event) => onDragStart(event, "Production")}>
        <span className="material-symbols-rounded">engineering</span>
      </div>
      
      <div className="item-end" title="End-node" draggable
           onClick={() => onCreateNewNode("End")}
           onDragStart={(event) => onDragStart(event, "End")}>
        <span className="material-symbols-rounded">local_shipping</span>
      </div>
      
      <div className="item-input" title="Input-node" draggable
           onClick={() => onCreateNewNode("Input")}
           onDragStart={(event) => onDragStart(event, "Spawn")}>
        <span className="material-symbols-rounded">exit_to_app</span>
      </div>
      
      <div className="item-worksheet" title="Worksheet-node" draggable
           onClick={() => onCreateNewNode("Worksheet")}
           onDragStart={(event) => onDragStart(event, "Production")}>
        <span className="material-symbols-rounded">factory</span>
      </div>
      
      <div className="item-output" title="Output-node" draggable
           onClick={() => onCreateNewNode("Output")}
           onDragStart={(event) => onDragStart(event, "End")}>
        <span className="material-symbols-rounded">output</span>
      </div>
    </div>
  )
}