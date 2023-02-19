import "./WorksheetOverview.css";
import React, {Component} from 'react';
import {WorksheetItem} from "./WorksheetItem";
import {WorksheetCreator} from "./WorksheetCreator";
import {fetchAllWorksheets} from "../../data/WorksheetAPI";
import Store from "../../data/DataStore";

export class WorksheetOverview extends Component {
  unsubscribe;
  
  constructor(props) {
    super(props);
    this.state = {
      worksheets: [],
      worksheetCreatorOpen:false,
      worksheetSearchType:"title",
      worksheetSearchQuery:"",
    }
    this.unsubscribe = Store.subscribe(() => {
      this.setState({worksheets: Store.getState().worksheets});
    });
    fetchAllWorksheets();
  }
  
  render () {
    let worksheetListFiltered = [...this.state.worksheets];
    let q = this.state.worksheetSearchQuery.toLowerCase();
    
    switch (this.state.worksheetSearchType) {
      case "title":
        worksheetListFiltered = worksheetListFiltered.filter(v => v.name.toLowerCase().includes(q));
        break;
      case "inputProduct":
        worksheetListFiltered = worksheetListFiltered.filter(v => v.inputProducts.filter(v => v.product.name.toLowerCase().includes(q)).length > 0);
        break;
      case "outputProduct":
        worksheetListFiltered = worksheetListFiltered.filter(v => v.outputProducts.filter(v => v.product.name.toLowerCase().includes(q)).length > 0);
        break;
    }
    
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
          <div className="filter-container">
            <select name="worksheetFilterType" title="Worksheet filter type" value={this.state.worksheetSearchType} onChange={e => this.setState({worksheetSearchType:e.target.value})}>
              <option value="title">Title</option>
              <option value="inputProduct">Input product</option>
              <option value="outputProduct">Output product</option>
            </select>
            <input placeholder="Search" type="text" value={this.state.worksheetSearchQuery} onChange={e => this.setState({worksheetSearchQuery: e.target.value})}/>
            <div className="flex-grow-1"></div>
            <button type="button" onClick={() => this.setState({worksheetCreatorOpen:true})}>Create new</button>
          </div>
          <div className="worksheet-item-container">
            {worksheetListFiltered.map(v => <div key={v.id} className="worksheet-item-wrapper"><WorksheetItem data={v} id={v.id}></WorksheetItem></div>)}
          </div>
        </div>
      </div>
    );
  }

  componentWillUnmount() {
    this.unsubscribe();
  }
}
