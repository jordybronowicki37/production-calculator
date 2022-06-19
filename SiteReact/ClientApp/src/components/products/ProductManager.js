import {Component} from "react";
import {ProductList} from "./ProductList";
import "./ProductManager.css";

export class ProductManager extends Component {
  constructor(props) {
    super(props);
    this.state = {
      products: [],
      newProductName:"",
    };
    
    this.fetchAll();
  }
  
  render() {
    return (
      <div>
        <div className="products">
          {ProductList(this.state.products)}
        </div>
      </div>
    );
  }
  
  fetchAll() {
    fetch("product/worksheet/1").then(response => response.json()).then(products => {
      this.setState({products: products});
    });
  }
}