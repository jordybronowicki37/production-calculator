﻿import {Component} from "react";
import "./ProductManager.css";
import Store from "../../dataStore/DataStore";
import {postNewProduct, deleteProduct} from "./ProductAPI";

export class ProductManager extends Component {
  unsubscribe;
  
  constructor(props) {
    super(props);
    this.state = {
      products: [],
      worksheetId: props.worksheetId,
    };
    this.unsubscribe = Store.subscribe(() => this.setState({products: Store.getState().product}));
  }
  
  render() {
    return (
      <div>
        <form className="product-creator" onSubmit={e => this.createNewProduct(e)}>
          <input name="product" type="text" autoComplete="off" placeholder="Product name"/>
          <button type="submit" title="Add product"><box-icon name='add-to-queue' type='solid' color='#96f378'></box-icon></button>
        </form>

        <ul className="products">
          {this.state.products.map(product => (
            <li key={product.name}>
              <div>{product.name}</div>
              <button className="product-remove-button" title="Remove product" onClick={e => this.removeProduct(e, product.name)}><box-icon type='solid' name='minus-circle' color="#ff8080"></box-icon></button>
            </li>))}
        </ul>
      </div>
    );
  }
  
  createNewProduct(e) {
    e.preventDefault()
    let name = e.target[0].value;
    e.target[0].value = "";
    postNewProduct(this.state.worksheetId, name);
  }
  
  removeProduct(e, name) {
    e.preventDefault();
    deleteProduct(this.state.worksheetId, name)
  }
  
  componentWillUnmount() {
    this.unsubscribe();
  }
}