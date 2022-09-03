import {Component} from "react";
import "./WorksheetCreator.css";
import {Link} from "react-router-dom";
import {createNewWorksheet} from "./WorksheetAPI";

export class WorksheetCreator extends Component {
  constructor(props) {
    super(props);
    
    this.state = {
      creationPageOpen:true,
      loadingPageOpen:false,
      successPageOpen:false,
      errorPageOpen:false,
      newWorksheetTitle:"",
      newWorksheetId:0,
    }
  }

  render() {
    return <div className="worksheet-creator">
      <div hidden={!this.state.creationPageOpen} className="creation-page">
        <form onSubmit={e => this.createWorksheet(e)}>
          <h3>Create a new worksheet</h3>
          <input placeholder="Worksheet name" title="Worksheet name" type="text" onChange={e => this.setState({newWorksheetTitle:e.target.value})}/>
          <button type="submit">Create</button>
        </form>
      </div>
      <div hidden={!this.state.loadingPageOpen} className="loading-page">
        <h3>Creating</h3>
        <div><i className='bx bx-loader-alt bx-spin'></i></div>
      </div>
      <div hidden={!this.state.successPageOpen} className="success-page">
        <h3>Success</h3>
        <div>Worksheet: "{this.state.newWorksheetTitle}" was created successfully</div>
        <div className="buttons">
          <button type="reset" onClick={e => this.reset(e)}>Create another</button>
          <Link to={`calculator/${this.state.newWorksheetId}`}><button type="button">Open</button></Link>
        </div>
      </div>
      <div hidden={!this.state.errorPageOpen} className="error-page">
        <div>Something went wrong during the creation of the worksheet.</div>
        <button type="reset" onClick={e => this.reset(e)}>Try again</button>
      </div>
    </div>
  }
  
  openPage(name) {
    let pages = {
      creationPageOpen:false,
      loadingPageOpen:false,
      successPageOpen:false,
      errorPageOpen:false,
    }
    
    switch (name) {
      case "creation":
        pages.creationPageOpen = true;
        break;
      case "loading":
        pages.loadingPageOpen = true;
        break;
      case "success":
        pages.successPageOpen = true;
        break;
      case "error":
        pages.errorPageOpen = true;
        break;
      default: return;
    }
    
    this.setState(pages)
  }
  
  createWorksheet(e) {
    e.preventDefault();
    this.openPage("loading");
    createNewWorksheet(this.state.newWorksheetTitle).then(r => {
      this.setState({
        newWorksheetTitle:r.name,
        newWorksheetId:r.id,
      });
      this.openPage("success");
    }).catch(r => {
      this.openPage("error");
    });
  }
  
  reset(e) {
    e.preventDefault();
    this.setState({
      creationPageOpen:true,
      loadingPageOpen:false,
      successPageOpen:false,
      errorPageOpen:false,
      newWorksheetTitle:"",
      newWorksheetId:0,
    });
  }
}