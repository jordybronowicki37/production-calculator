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
        <form className="product-creator" onSubmit={e => this.createNewProduct(e)}>
          <input name="product" type="text" autoComplete="off" placeholder="Product name" onInput={e => this.setState({newProductName: e.target.value})}/>
          <button type="submit">Add</button>
        </form>
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
  
  createNewProduct(e) {
    e.preventDefault()
    let name = e.target[0].value;
    
    fetch("product/worksheet/1", {
        method: "post",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({name: name})
      }).then(r => this.fetchAll());
   }
}