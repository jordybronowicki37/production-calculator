import "./UserNotificationManager.scss";
import React, {useEffect, useState} from "react";
import {StoreStates} from "../../data/DataStore";
import {NotificationTypes} from "../../data/reducers/NotificationReducer";
import {useSelector} from "react-redux";

export function UserNotificationManager(): React.JSX.Element {
  const [lastTimeStamp, setLastTimeStamp] = useState<Date>(new Date());
  const [notifications, setNotifications] = useState<NotificationExtended[]>([]);

  const allNotifications = useSelector<StoreStates, StoreStates["notifications"]>(state => state.notifications);

  useEffect(() => {
    const recentNotifications = allNotifications.filter(v => {
      if (v.time <= lastTimeStamp) return false;
      setLastTimeStamp(v.time);
      return true;
    }).map(v => ({...v, disappearing: false}) as NotificationExtended);

    recentNotifications.forEach(v => {
      v.disappearing = false;
      setTimeout(() => {
        v.disappearing = true;
        setTimeout(() => setNotifications((n) => n.filter(t => t.id !== v.id)), 1000);
      }, 5000);
    });
    setNotifications(n => [...n, ...recentNotifications]);
  }, [allNotifications, lastTimeStamp]);

  return <div className="user-notification-manager">
    {notifications.map(v => {
      return <div key={v.id} className={`${v.type} ${v.disappearing ? "disappearing" : ""}`}>
        <div className="icon-wrapper">
          <div hidden={v.type !== "info"}><i className='bx bx-info-circle'></i></div>
          <div hidden={v.type !== "success"}><i className='bx bx-check'></i></div>
          <div hidden={v.type !== "warning"}><i className='bx bx-error'></i></div>
          <div hidden={v.type !== "error"}><i className='bx bx-error-circle'></i></div>
        </div>
        <div className="text-wrapper">{v.message}</div>
      </div>
    })}
  </div>
}

type NotificationExtended = {
  id: number,
  time: Date,
  type: NotificationTypes,
  message: string,
  disappearing: boolean,
}
