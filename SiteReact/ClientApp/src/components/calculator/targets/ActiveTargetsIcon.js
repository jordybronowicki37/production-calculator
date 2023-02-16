import "./ActiveTargetsIcon.css";

export function ActiveTargetsIcon({targets, onOpenEditor}) {
  let targetIcon;
  
  if (targets.length !== 0) {
    if (targets[0].type === "ExactAmount") {
      targetIcon = <div>
        <i className='bx bx-arrow-to-right right-one'></i>
        <i className='bx bx-arrow-to-left left-one'></i>
      </div>;
    } else if (targets[0].type === "MinAmount" || targets[0].type === "MaxAmount") {
      targetIcon = <div>
        <i className='bx bx-arrow-to-left right-one'></i>
        <i className='bx bx-arrow-to-right left-one'></i>
      </div>;
    }
  }
  
  return (
    <div className="active-target-icon" onClick={onOpenEditor}>
      {targetIcon}
      <i className='bx bx-target-lock'></i>
    </div>
  );
}