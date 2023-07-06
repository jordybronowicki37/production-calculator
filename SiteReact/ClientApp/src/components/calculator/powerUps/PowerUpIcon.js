import "./PowerUpIcon.scss";

export function PowerUpIcon({powerUps, onOpenEditor}) {
  return (
    <div
      title="Edit power ups"
      className={`power-up-icon ${powerUps.length === 0 ? "": "active"}`}
      onClick={onOpenEditor}>
      <span className="material-icons">rocket_launch</span>
    </div>
  );
}
