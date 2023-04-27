import "./Layout.css";
import React, {Component} from 'react';
import {NavMenu} from './NavMenu';
import {UserNotificationManager} from "../notification/UserNotificationManager";

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div className="layout min-vh-100">
        <NavMenu />
        <div className="notification-wrapper">
          <UserNotificationManager/>
        </div>
        {this.props.children}
      </div>
    );
  }
}
