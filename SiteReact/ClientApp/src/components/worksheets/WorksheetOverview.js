import "./WorksheetOverview.css";
import React, {useState} from 'react';
import {WorksheetItem} from "./WorksheetItem";
import {WorksheetCreator} from "./WorksheetCreator";
import {Popup} from "../popup/Popup";

export function WorksheetOverview({worksheets}) {
  const [creatorOpen, setCreatorOpen] = useState(false);
  const [searchType, setSearchType] = useState("title");
  const [searchQuery, setSearchQuery] = useState("");

  let worksheetsFiltered = [...worksheets];
  let q = searchQuery.toLowerCase();

  switch (searchType) {
    case "title":
      worksheetsFiltered = worksheetsFiltered.filter(v => v.name.toLowerCase().includes(q));
      break;
    case "inputProduct":
      worksheetsFiltered = worksheetsFiltered.filter(v => v.inputProducts.filter(v => v.product.name.toLowerCase().includes(q)).length > 0);
      break;
    case "outputProduct":
      worksheetsFiltered = worksheetsFiltered.filter(v => v.outputProducts.filter(v => v.product.name.toLowerCase().includes(q)).length > 0);
      break;
  }

  return (
    <div className="worksheet-overview">
      <Popup
        onClose={() => setCreatorOpen(false)}
        hidden={!creatorOpen}>
        <WorksheetCreator/>
      </Popup>

      <div>
        <div className="filter-container">
          <select name="worksheetFilterType" title="Worksheet filter type" value={searchType} onChange={e => setSearchType(e.target.value)}>
            <option value="title">Title</option>
            <option value="inputProduct">Input product</option>
            <option value="outputProduct">Output product</option>
          </select>
          <input placeholder="Search" type="text" value={searchQuery} onChange={e => setSearchQuery(e.target.value)}/>
          <div className="flex-grow-1"/>
          <button type="button" onClick={() => setCreatorOpen(true)}>Create new</button>
        </div>
        <div className="worksheet-item-container">
          {worksheetsFiltered.map(w => <WorksheetItem key={w.id} worksheet={w}/>)}
        </div>
      </div>
    </div>
  );
}
