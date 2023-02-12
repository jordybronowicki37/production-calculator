import React, {Component} from 'react';
import {Redirect} from "react-router-dom";

export class HomePage extends Component {
  render () {
    return (
      <div>
        <Redirect to="/calculator"/>
      </div>
    );
  }
}
