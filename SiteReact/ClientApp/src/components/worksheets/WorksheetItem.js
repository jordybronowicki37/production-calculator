import React, { Component } from 'react';
import {Link} from "react-router-dom";
import "./WorksheetItem.css";

export class WorksheetItem extends Component {
  constructor(props) {
    super(props);
    const {name} = props.data;
    this.state = {
      id: props.id,
      name,
    }
  }

  render () {
    return (
      <div className="worksheetItemContainer">
        <Link className="worksheetItemTitle" to={`calculator/${this.state.id}`}>{this.state.name}</Link>
      </div>
    );
  }
}
