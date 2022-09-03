import React, { Component } from 'react';
import {WorksheetItem} from "./WorksheetItem";
import {fetchAllWorksheets} from "./WorksheetAPI";
import Store from "../../dataStore/DataStore";
import "./WorksheetOverview.css";
import {WorksheetCreator} from "./WorksheetCreator";

export class WorksheetOverview extends Component {
  unsubscribe;
  
  constructor(props) {
    super(props);
    this.state = {
      worksheets: [],
      worksheetCreatorOpen:false,
    }
    this.unsubscribe = Store.subscribe(() => {
      this.setState({worksheets: Store.getState().worksheets});
    });
    fetchAllWorksheets();
  }
  
  render () {
    return (
      <div className="worksheet-overview">
        <div hidden={!this.state.worksheetCreatorOpen} className="popup-container" onClick={() => this.setState({worksheetCreatorOpen:false})}>
          <div onClick={e => e.stopPropagation()}>
            <button type="button" className="popup-close-button" onClick={() => this.setState({worksheetCreatorOpen:false})}>
              <i className='bx bx-x'></i>
            </button>
            <WorksheetCreator></WorksheetCreator>
          </div>
        </div>
        
        <div>
          <h1>Worksheet overview</h1>
          <button type="button" onClick={() => this.setState({worksheetCreatorOpen:true})}>Create new</button>
          <div>
            {this.state.worksheets.map(v => <div key={v.name} className="worksheetItem"><WorksheetItem data={v} id={v.id}></WorksheetItem></div>)}
          </div>
        </div>
      </div>
    );
  }

  componentWillUnmount() {
    this.unsubscribe();
  }
}
