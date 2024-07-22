import "./Layout.css";
import {NavMenu} from './NavMenu';
import {UserNotificationManager} from "../notification/UserNotificationManager";
import React from "react";

export function Layout({children}: { children: React.JSX.Element | React.JSX.Element[] }): React.JSX.Element {
  return (
    <div className="layout min-vh-100">
      <NavMenu/>
      <div className="notification-wrapper">
        <UserNotificationManager/>
      </div>
      {children}
    </div>
  );
}
