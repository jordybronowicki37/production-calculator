import "./ErrorAlertIcon.scss";
import {Alert, AlertLevel} from "../../../data/DataTypes";
import React from "react";

export type ErrorAlertIconProps = {
  alerts: Alert[], 
  onOpenEditor?: () => void,
}

export function ErrorAlertIcon({alerts, onOpenEditor}: ErrorAlertIconProps): React.JSX.Element {
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
