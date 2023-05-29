import {WorksheetItem} from "../../worksheets/WorksheetItem";
import "./SelectorTab.css";

export function SelectorTab({worksheets, products, onAddTab}) {
  return (
    <div className="worksheet-selector">
      <h2>Select a worksheet</h2>
      <div className="worksheet-list">
        {worksheets.map(v => <div key={v.id} onClick={() => onAddTab(v.id)}><WorksheetItem worksheet={v} products={products}/></div>)}
      </div>
    </div>
  );
}
