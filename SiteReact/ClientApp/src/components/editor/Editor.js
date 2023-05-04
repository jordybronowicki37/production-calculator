import "./Editor.css";
import {Component} from "react";
import {WorksheetItem} from "../worksheets/WorksheetItem";
import Store from "../../data/DataStore";

export class Editor extends Component {
  constructor(props) {
    super(props);
    const {project} = props;
    console.log(project);
    console.log(Store.getState())
    this.state = {
      project: project,
      tabs: [
        {
          name: "calculator"
        }
      ],
    }
  }
  
  render() {
    return (
      <div className="editor">
        
        <div className="tabs">
          <button className="tab selected">calculator</button>
          <button className="add"><span className="material-symbols-rounded">add</span></button>
        </div>
        <div className="tabs_content">
          <div>
            
          </div>
          <div >
            <WorksheetSelector project={this.state.project}/>
          </div>
        </div>
      </div>
    );
  }
}

function WorksheetSelector({project}) {
  return (
    <div className="worksheet-selector">
      <h2>Select a worksheet</h2>
      <div className="worksheet-list">
        {project.worksheets.map(v => <WorksheetItem key={v.id} worksheet={v}/>)}
      </div>
    </div>
  );
}
