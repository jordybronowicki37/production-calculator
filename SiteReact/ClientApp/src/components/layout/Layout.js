import "./Layout.css";
import React, {Component} from 'react';
import {Container} from 'reactstrap';
import {NavMenu} from './NavMenu';
import {UserNotificationManager} from "../notification/UserNotificationManager";

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div className="layout">
        <NavMenu />
        <Container>
          <div className="notification-wrapper">
            <UserNotificationManager/>
          </div>
          {this.props.children}
        </Container>
      </div>
    );
  }
}