import "./Editor.css";
import {WorksheetItem} from "../worksheets/WorksheetItem";
import {Calculator} from "../calculator/Calculator";
import {fetchWorksheet} from "../../data/api/WorksheetAPI";

export function Editor({ project, worksheets, products, recipes, machines }) {
  let worksheet = worksheets[0];
  
  return (
    <div className="editor">
      
      <div className="tabs">
        <button className="tab selected">calculator</button>
        <button className="add"><span className="material-symbols-rounded">add</span></button>
      </div>
      <div className="tabs_content">
        {worksheet != null && <Calculator worksheet={worksheet} products={products} recipes={recipes}/>}
        
        {/*<div >*/}
        {/*  <WorksheetSelectorTab project={this.state.project}/>*/}
        {/*</div>*/}
      </div>
    </div>
  );
}

function WorksheetSelectorTab({project, products}) {
  return (
    <div className="worksheet-selector">
      <h2>Select a worksheet</h2>
      <div className="worksheet-list">
        {project.worksheets.map(v => <WorksheetItem key={v.id} worksheet={v} products={products}/>)}
      </div>
    </div>
  );
}

async function LoadWorksheet(id) {
  return await fetchWorksheet(id);
}
