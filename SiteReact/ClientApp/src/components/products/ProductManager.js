import {Component} from "react";
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
    this.unsubscribe = Store.subscribe(() => this.setState({products: Store.getState().products}));
  }
  
  render() {
    return (
      <div>
        <form className="product-creator" onSubmit={e => this.createNewProduct(e)}>
          <input name="product" type="text" autoComplete="off" placeholder="Product name"/>
          <button type="submit" title="Add product">
            <i className='bx bxs-add-to-queue' style={{color:"#96f378"}}/>
          </button>
        </form>

        <ul className="products">
          {this.state.products.map(product => (
            <li key={product.name}>
              <div>{product.name}</div>
              <button className="product-remove-button" title="Remove product" onClick={e => this.removeProduct(e, product.name)}>
                <i className='bx bxs-minus-circle' style={{color:"#ff8080"}}/>
              </button>
            </li>))}
        </ul>
      </div>
    );
  }
  
  createNewProduct(e) {
    e.preventDefault()
    let name = e.target[0].value;
    e.target[0].value = "";
    postNewProduct(name);
  }
  
  removeProduct(e, name) {
    e.preventDefault();
    deleteProduct(name)
  }
  
  componentWillUnmount() {
    this.unsubscribe();
  }
}