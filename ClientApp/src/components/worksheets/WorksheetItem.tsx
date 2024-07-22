import "./WorksheetItem.scss";
import {Product, Worksheet, Node} from "../../data/DataTypes";
import {RoundedAmountField} from "../misc/RoundedAmountField";
import React from "react";

export type WorksheetItemProps = {
  worksheet: Worksheet, 
  products: Product[],
}

export function WorksheetItem({worksheet, products}: WorksheetItemProps): React.JSX.Element {
  const {name, nodes} = worksheet;
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

function ThroughputItem(throughput: ThroughputExpanded) {
  return (
    <li key={throughput.product} className="worksheet-item-throughput">
      <div>{throughput.name}</div>
      <RoundedAmountField amount={throughput.amount}/>
    </li>
  );
}

type ThroughputExpanded = {
  product: string,
  name: string,
  amount: number,
}

function generateWorksheetThroughput(nodes: Node[], products: Product[]): ThroughputExpanded[] {
  const dict: {[key: string]:ThroughputExpanded} = {};
  for (const node of nodes) {
    if (!node.product) continue;
    const v = dict[node.product];
    if (v){
      v.amount += node.amount;
      dict[node.product] = v;
    } else {
      const fp = findProduct(products, node.product);
      if (!fp) continue;
      dict[node.product] = {
        product: node.product,
        amount: node.amount,
        name: fp.name
      }
    }
  }
  return Object.values(dict);
}

function findProduct(list: Product[], id: string): Product | undefined {
  return list.find(v => v.id === id);
}
