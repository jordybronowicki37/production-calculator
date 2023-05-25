import "./Editor.css";
import {WorksheetItem} from "../worksheets/WorksheetItem";
import {Calculator} from "../calculator/Calculator";
import {fetchWorksheet} from "../../data/api/WorksheetAPI";
import {useEffect} from "react";

export function Editor({ project, worksheet, products, recipes, machines }) {
  useEffect(() => {
    LoadWorksheet(project.worksheets[0].id)
  }, []);
  
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

function WorksheetSelectorTab({project}) {
  return (
    <div className="worksheet-selector">
      <h2>Select a worksheet</h2>
      <div className="worksheet-list">
        {project.worksheets.map(v => <WorksheetItem key={v.id} worksheet={v}/>)}
      </div>
    </div>
  );
}

async function LoadWorksheet(id) {
  return await fetchWorksheet(id);
}
