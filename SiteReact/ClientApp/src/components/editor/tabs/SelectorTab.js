import {WorksheetItem} from "../../worksheets/WorksheetItem";
import "./SelectorTab.scss";
import {Button} from "reactstrap";
import {Popup} from "../../popup/Popup";
import {useState} from "react";
import {WorksheetCreator} from "../../worksheets/WorksheetCreator";

export function SelectorTab({projectId, worksheets, products, onAddTab, openedWorksheetIds}) {
  const [createWorksheetOpen, setCreateWorksheetOpen] = useState(false);
  
  worksheets = [...worksheets].sort((a,b) => (a.name > b.name) ? 1 : (b.name > a.name) ? -1 : 0);
  
  const notOpenedWorksheets = worksheets.filter(w => !openedWorksheetIds.includes(w.id));
  const openedWorksheets = worksheets.filter(w => openedWorksheetIds.includes(w.id));
  
  return (
    <div className="worksheet-selector">
      <Popup
        onClose={() => setCreateWorksheetOpen(false)}
        hidden={!createWorksheetOpen}>
        <WorksheetCreator
          handleResult={v => {
            onAddTab(v.id);
            setCreateWorksheetOpen(false);
          }}
          projectId={projectId}/>
      </Popup>
      
      {
        worksheets.length === 0 ?
          <NoWorksheetsYet onClick={() => setCreateWorksheetOpen(true)}/> :
          <>
            <div>
              <div className="worksheet-list-title-container">
                <h2>Open a worksheet</h2>
                <Button color="light" outline onClick={() => setCreateWorksheetOpen(true)}>
                  New
                </Button>
              </div>

              {
                notOpenedWorksheets.length === 0 ?
                  <NoMoreUnopenedWorksheets/> :
                  <div className="worksheet-list">
                    {notOpenedWorksheets.map(v => <div className="worksheet-item-wrapper" key={v.id} onClick={() => onAddTab(v.id)}><WorksheetItem worksheet={v} products={products}/></div>)}
                  </div>
              }
            </div>

            {
              openedWorksheets.length !== 0 &&
              <div>
                <div className="worksheet-list-title-container">
                  <h2>Already opened worksheets</h2>
                </div>
                <div className="worksheet-list">
                  {openedWorksheets.map(v => 
                    <div className="worksheet-item-wrapper" key={v.id} onClick={() => onAddTab(v.id)}>
                      <WorksheetItem worksheet={v} products={products}/>
                    </div>)}
                </div>
              </div>
            }
          </>
      }
    </div>
  );
}

function NoWorksheetsYet({onClick}) {
  return <div className="no-worksheet-yet">
    <label>You do not have any worksheets yet</label>
    <Button color="light" outline onClick={onClick}>Create new worksheet</Button>
  </div>
}

function NoMoreUnopenedWorksheets() {
  return <div className="p-3">
    No more unopened worksheets
  </div>
}
