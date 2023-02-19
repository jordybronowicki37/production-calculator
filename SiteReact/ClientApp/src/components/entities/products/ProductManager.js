import {useMemo, useState} from "react";
import "./ProductManager.css";
import {deleteProduct, postNewProduct} from "../../../data/api/ProductAPI";

export function ProductManager({products}) {
  const [filter, setFilter] = useState("");
  
  const productsSorted = useMemo(() => {
    return sortProducts(products);
  }, [products]);
  
  let productsFiltered = productsSorted.filter(v => v.name.toLowerCase().includes(filter.toLowerCase()));

  return (
    <div className="product-manager">
      <h3>Products</h3>

      <div className="separation-line"></div>

      <form className="creator" onSubmit={handleCreateNewProduct}>
        <input name="product" type="text" autoComplete="off" placeholder="Product name"/>
        <button type="submit" title="Add product">
          <i className='bx bxs-add-to-queue' style={{color:"#96f378"}}/>
        </button>
      </form>

      <div className="separation-line"></div>

      <form className="filter">
        <input placeholder="Filter products" type="text" onChange={e => setFilter(e.target.value)}/>
      </form>

      <div className="separation-line"></div>

      <ul className="products">
        {productsFiltered.map(product => (
          <li key={product.name}>
            <div>{product.name}</div>
            <button type="button" className="remove-button" title="Remove product" onClick={e => handleRemoveProduct(e, product.name)}>
              <i className='bx bxs-minus-circle' style={{color:"#ff8080"}}/>
            </button>
          </li>))}
      </ul>
    </div>
  );
}

function sortProducts(products) {
  return [...products].sort((v1, v2) => {
    const n1 = v1.name;
    const n2 = v2.name;
    if (n1 > n2) return 1;
    if (n1 < n2) return -1;
    return 0
  });
}

function handleCreateNewProduct(e) {
  e.preventDefault()
  let name = e.target[0].value;
  e.target[0].value = "";
  postNewProduct(name);
}

function handleRemoveProduct(e, name) {
  e.preventDefault();
  deleteProduct(name)
}