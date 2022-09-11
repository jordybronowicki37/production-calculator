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
      worksheetId: this.sortProducts(Store.getState().products),
      filter:"",
    };
    this.unsubscribe = Store.subscribe(() => this.setState({products: this.sortProducts(Store.getState().products)}));
  }
  
  render() {
    let productsFiltered = this.state.products.filter(v => v.name.toLowerCase().includes(this.state.filter.toLowerCase()));
    
    return (
      <div className="product-manager">
        <h3>Products</h3>
        
        <div className="separation-line"></div>

        <form className="product-creator" onSubmit={e => this.createNewProduct(e)}>
          <input name="product" type="text" autoComplete="off" placeholder="Product name"/>
          <button type="submit" title="Add product">
            <i className='bx bxs-add-to-queue' style={{color:"#96f378"}}/>
          </button>
        </form>

        <div className="separation-line"></div>

        <form className="product-filter">
          <input placeholder="Filter products" type="text" onChange={e => this.setState({filter: e.target.value})}/>
        </form>

        <div className="separation-line"></div>

        <ul className="products">
          {productsFiltered.map(product => (
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
  
  sortProducts(products) {
    return [...products].sort((v1, v2) => {
      const n1 = v1.name;
      const n2 = v2.name;
      if (n1 > n2) return 1;
      if (n1 < n2) return -1;
      return 0
    });
  }
  
  componentWillUnmount() {
    this.unsubscribe();
  }
}