import "./ActiveTargetsIcon.scss";
import {ProductionTarget} from "../../../data/DataTypes";

export function ActiveTargetsIcon({targets, onOpenEditor}:{targets: ProductionTarget[], onOpenEditor: () => void}) {
  return (
    <div 
      title="Edit targets"
      className={`active-target-icon ${targets.length === 0 ? "": "active"}`} 
      onClick={onOpenEditor}>
      <span className="material-icons">bolt</span>
    </div>
  );
}
