import "./WorksheetItem.scss";
import React from 'react';

export function WorksheetItem({worksheet, products}) {
  const {id, name, nodes, connections} = worksheet;
  const inputNodes = nodes.filter(n => n.type === "Spawn");
  const outputNodes = nodes.filter(n => n.type === "End");
  const inputProducts = generateWorksheetThroughput(inputNodes, products);
  const outputProducts = generateWorksheetThroughput(outputNodes, products);

  return (
    <div className="worksheet-item">
      <div className="top-container">
        <div className="title">{name}</div>
      </div>
      <div className="amounts-container">
        <div>Nodes:</div>
        <div>{nodes.length}</div>
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

function generateWorksheetThroughput(nodes, products) {
  const dict = {};
  for (const node of nodes) {
    let v = dict[node.product];
    if (v){
      v.amount += node.amount;
      dict[node.product] = v;
    } else {
      dict[node.product] = {
        product: node.product,
        amount: node.amount,
        name: findProduct(products, node.product).name
      }
    }
  }
  return Object.values(dict);
}

function findProduct(list, id) {
  return list.find(v => v.id === id);
}
