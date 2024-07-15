import "./ErrorAlertIcon.scss";
import {Alert, AlertLevel} from "../../../data/DataTypes.d.ts";

export function ErrorAlertIcon({alerts, onOpenEditor}: { alerts: Alert[], onOpenEditor: () => void }) {
  alerts = alerts.filter(a => a.level === AlertLevel.Error);

  return (
    <>
      {alerts.length > 0 &&
          <div
              title="View errors"
              className="error-alert-icon"
              onClick={onOpenEditor}>
              <i className='bx bx-error'/>
              <div><p>{alerts.length}</p></div>
          </div>
      }
    </>
  );
}
