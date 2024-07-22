import "./Editor.scss";
import React, {useState} from "react";
import {SelectorTab} from "./tabs/SelectorTab";
import {CalculatorTab} from "./tabs/CalculatorTab";
import {Machine, Product, Project, Recipe, Worksheet} from "../../data/DataTypes";

export type EditorProps = {
  project: Project,
  worksheets: Worksheet[],
  products: Product[],
  recipes: Recipe[],
  machines: Machine[],
}

export function Editor({project, worksheets, products, recipes, machines}: EditorProps): React.JSX.Element {
  const [tabs, setTabs] = useState<EditorTab[]>([]);
  const [selectedTab, setSelectedTab] = useState<number>(-1);
  const openedWorksheets = tabs.map(t => t.id);
  const currecntWorksheet = getWorksheet(worksheets, tabs[selectedTab].id);

  return (
    <div className="editor">
      <div className="tabs-wrapper">
        <div className="tabs-container">
          {
            tabs.map((t, i) => {
              const worksheet = getWorksheet(worksheets, t.id);
              if (worksheet == undefined) return <div>Unknown worksheet</div>
              return <div key={t.id} className={`editor-tab ${i === selectedTab ? "selected" : ""}`}>
                <button
                  className={`tab-title`}
                  onClick={() => setSelectedTab(i)}
                >{worksheet.name}</button>
                <button
                  title="Close worksheet"
                  className="editor-tab-close"
                  onClick={() => {
                    if (i <= selectedTab) setSelectedTab(selectedTab - 1);
                    setTabs(tabs.filter((_, j) => j !== i));
                  }}>
                  <span className="material-symbols-rounded">close</span>
                </button>
              </div>
            })
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
                  const i = tabs.findIndex(v => v.id === id);
                  setSelectedTab(i);
                } else {
                  setTabs([...tabs, {type: "calculator", id}]);
                  setSelectedTab(tabs.length);
                }
              }}/> :
            currecntWorksheet == undefined ?
              <div>Unknown worksheet</div> :
              <CalculatorTab
                worksheet={currecntWorksheet}
                products={products}
                recipes={recipes}
                machines={machines}/>
        }
      </div>
    </div>
  );
}

export type EditorTabTypes = "calculator";

export type EditorTab = {
  id: string,
  type: EditorTabTypes,
}

function getWorksheet(worksheets: Worksheet[], id: string): Worksheet | undefined {
  return worksheets.find(w => w.id === id);
}
