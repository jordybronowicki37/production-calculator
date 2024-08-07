import "./PowerUpIcon.scss";
import React from "react";

export type PowerUpIconProps = {
  powerUps: [], 
  onOpenEditor?: () => void,
}

export function PowerUpIcon({powerUps, onOpenEditor}: PowerUpIconProps): React.JSX.Element {
  return (
    <div
      title="Edit power ups"
      className={`power-up-icon ${powerUps.length === 0 ? "": "active"}`}
      onClick={onOpenEditor}>
      <span className="material-icons">rocket_launch</span>
    </div>
  );
}
