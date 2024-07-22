import "./ActiveTargetsIcon.scss";
import {ProductionTarget} from "../../../data/DataTypes";
import React from "react";

export type ActiveTargetsIconProps = {
  targets: ProductionTarget[], 
  onOpenEditor: () => void,
}

export function ActiveTargetsIcon({targets, onOpenEditor}: ActiveTargetsIconProps): React.JSX.Element {
  return (
    <div 
      title="Edit targets"
      className={`active-target-icon ${targets.length === 0 ? "": "active"}`} 
      onClick={onOpenEditor}>
      <span className="material-icons">bolt</span>
    </div>
  );
}
