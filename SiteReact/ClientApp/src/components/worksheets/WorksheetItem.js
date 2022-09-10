import React, { Component } from 'react';
import {Link} from "react-router-dom";
import "./WorksheetItem.css";

export class WorksheetItem extends Component {
  constructor(props) {
    super(props);
    const {name, amountProducts, amountRecipes, amountNodes, inputProducts, outputProducts} = props.data;
    
    this.state = {
      id: props.id, 
      name, amountProducts, amountRecipes, amountNodes, inputProducts, outputProducts,
    }
  }

  render () {
    const {id, name, amountProducts, amountRecipes, amountNodes, inputProducts, outputProducts} = this.state;
    
    let inputs = inputProducts.map(v => 
      <li key={v.product.name} className="worksheet-item-throughput">
        <div>{v.product.name}</div>
        <div>{v.amount}</div>
      </li>);
    let outputs = outputProducts.map(v =>
      <li key={v.product.name} className="worksheet-item-throughput">
        <div>{v.product.name}</div>
        <div>{v.amount}</div>
      </li>);
    
    return (
      <div className="worksheet-item">
        <div className="worksheet-item-top">
          <Link className="worksheet-item-title" to={`calculator/${id}`}>{name}</Link>
          <Link className="worksheet-item-open-icon" to={`calculator/${id}`}><i className='bx bx-folder-open bx-tada-hover'></i></Link>
        </div>
        <div className="worksheet-item-amounts">
          <div>Products:</div>
          <div>{amountProducts}</div>
          <div>Recipes:</div>
          <div>{amountRecipes}</div>
          <div>Nodes:</div>
          <div>{amountNodes}</div>
        </div>
        <div className="worksheet-item-io" hidden={inputProducts.length===0 && outputProducts.length===0}>
          <div>Inputs</div>
          <div>Outputs</div>
          <ul>
            {inputs}
          </ul>
          <ul>
            {outputs}
          </ul>
        </div>
      </div>
    );
  }
}
