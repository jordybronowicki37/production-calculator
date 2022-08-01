import React, { Component } from 'react';
import {WorksheetItem} from "./WorksheetItem";
import {fetchAllWorksheets} from "./WorksheetAPI";
import Store from "../../dataStore/DataStore";
import "./WorksheetOverview.css";

export class WorksheetOverview extends Component {
  unsubscribe;
  
  constructor(props) {
    super(props);
    this.state = {
      worksheets: []
    }
    this.unsubscribe = Store.subscribe(() => {
      this.setState({worksheets: Store.getState().worksheets});
      console.log(this.state)
    });
    fetchAllWorksheets();
  }
  
  render () {
    return (
      <div>
        <h1>Worksheet overview</h1>
        <div>
          {this.state.worksheets.map((v, i) => <div className="worksheetItem"><WorksheetItem key={v.name} data={v} id={i}></WorksheetItem></div>)}
        </div>
      </div>
    );
  }

  componentWillUnmount() {
    this.unsubscribe();
  }
}
