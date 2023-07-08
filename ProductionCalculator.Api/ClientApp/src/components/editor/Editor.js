import "./Editor.scss";
import {useState} from "react";
import {SelectorTab} from "./tabs/SelectorTab";
import {CalculatorTab} from "./tabs/CalculatorTab";

export function Editor({ project, worksheets, products, recipes, machines }) {
  const [tabs, setTabs] = useState([]);
  const [selectedTab, setSelectedTab] = useState(-1);
  const openedWorksheets = tabs.map(t => t.id);

  return (
    <div className="editor">
      <div className="tabs-wrapper">
        <div className="tabs-container">
          {
            tabs.map((t, i) => <div key={t.id} className={`editor-tab ${i === selectedTab ? "selected" : ""}`}>
              <button
                className={`tab-title`}
                onClick={() => setSelectedTab(i)}
              >{getWorksheet(worksheets, t.id).name}</button>
              <button 
                title="Close worksheet"
                className="editor-tab-close"  
                onClick={() => {
                  if (i <= selectedTab) setSelectedTab(selectedTab - 1);
                  setTabs(tabs.filter((_, j) => j !== i));
              }}>
                <span className="material-symbols-rounded">close</span>
              </button>
            </div>)
          }
        </div>
        <button
          type="button"
          title="Open worksheet"
          className={`editor-tab-add ${selectedTab === -1 ? "selected" : ""}`}
          onClick={() => setSelectedTab(-1)}>
          <span className="material-symbols-rounded">add</span>
        </button>
      </div>
      
      <div className="tabs-content">
        {
          selectedTab === -1 ?
            <SelectorTab
              projectId={project.id}
              worksheets={worksheets} 
              products={products}
              openedWorksheetIds={tabs.filter(t => t.type === "calculator").map(t => t.id)}
              onAddTab={id => {
                if (openedWorksheets.includes(id)) {
                  let i = tabs.findIndex(v => v.id === id);
                  setSelectedTab(i);
                } else {
                  setTabs([...tabs, {type:"calculator", id}]);
                  setSelectedTab(tabs.length);
                }
              }}/> :
            <CalculatorTab 
              worksheet={getWorksheet(worksheets, tabs[selectedTab].id)} 
              products={products} 
              recipes={recipes}
              machines={machines}/>
        }
      </div>
    </div>
  );
}

function getWorksheet(worksheets, id) {
  return worksheets.find(w => w.id === id);
}
