import "./WorksheetItem.css";
import React from 'react';
import {Link} from "react-router-dom";
import {useSelector} from "react-redux";

export function WorksheetItem({worksheet}) {
  const {id, name, amountNodes, inputProducts, outputProducts} = worksheet;
  
  return (
    <div className="worksheet-item">
      <div className="top-container">
        <Link className="title" to={`/calculator/${id}`}>{name}</Link>
        <Link className="open-icon" to={`/calculator/${id}`}><i className='bx bx-folder-open bx-tada-hover'></i></Link>
      </div>
      <div className="amounts-container">
        <div>Nodes:</div>
        <div>{amountNodes}</div>
      </div>
      <div className="input-output-container" hidden={inputProducts.length===0 && outputProducts.length===0}>
        <div>Inputs</div>
        <div>Outputs</div>
        <ul>
          {inputProducts.map(t => ThroughputItem(t))}
        </ul>
        <ul>
          {outputProducts.map(t => ThroughputItem(t))}
        </ul>
      </div>
    </div>
  );
}

function ThroughputItem(throughput) {
  return (
    <li key={throughput.product} className="worksheet-item-throughput">
      <div>{throughput.name}</div>
      <div>{throughput.amount}</div>
    </li>
  );
}

function findProduct(list, id) {
  return list.find(v => v.id === id);
}
