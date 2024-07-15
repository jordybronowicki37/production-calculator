import "./WarningAlertIcon.scss";
import {Alert, AlertLevel} from "../../../data/DataTypes.d.ts";

export function WarningAlertIcon({alerts, onOpenEditor}: {alerts: Alert[], onOpenEditor: () => void}) {
  alerts = alerts.filter(a => a.level === AlertLevel.Warning);
  
  return (
    <>
      {alerts.length > 0 &&
          <div
              title="View warnings"
              className="warning-alert-icon"
              onClick={onOpenEditor}>
              <i className='bx bx-error-circle'/>
              <div><p>{alerts.length}</p></div>
          </div>
      }
    </>
  );
}
