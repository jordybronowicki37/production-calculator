import React, { Component } from 'react';
import {Redirect} from "react-router-dom";

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <Redirect to="/calculator"/>
      </div>
    );
  }
}
