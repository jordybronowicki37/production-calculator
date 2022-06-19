import {Component} from "react";
import "./ProductManager.css";

export class ProductManager extends Component {
  constructor(props) {
    super(props);
    this.state = {
      products: [],
    };
    
    this.fetchAll();
  }
  
  render() {
    return (
      <div>
        <form className="product-creator" onSubmit={e => this.createNewProduct(e)}>
          <input name="product" type="text" autoComplete="off" placeholder="Product name"/>
          <button type="submit">Add</button>
        </form>

        <ul className="products">
          {this.state.products.map(product => (<li key={product.name}>{product.name}</li>))}
        </ul>
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