import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import {UserNotificationManager} from "./notification/UserNotificationManager";
import "./Layout.css";

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu />
        <Container>
          <div className="notification-wrapper">
            <UserNotificationManager></UserNotificationManager>
          </div>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
