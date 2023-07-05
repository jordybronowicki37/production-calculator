import "./ActiveTargetsIcon.css";

export function ActiveTargetsIcon({targets, onOpenEditor}) {
  return (
    <div 
      title="Edit targets"
      className={`active-target-icon ${targets.length === 0 ? "": "active"}`} 
      onClick={onOpenEditor}>
      <span className="material-icons">bolt</span>
    </div>
  );
}