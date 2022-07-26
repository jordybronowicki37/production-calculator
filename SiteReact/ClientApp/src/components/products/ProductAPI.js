import Store from "../../dataStore/DataStore";

export const fetchAllProducts = async function(worksheetId) {
  let response = await fetch(`product/worksheet/${worksheetId}`);
  let data = await response.json();
  Store.dispatch({type:"product/set", payload:data});
}

export const postNewProduct = async function(worksheetId, productName) {
  await fetch(`product/worksheet/${worksheetId}`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({name: productName})
  });
  await this.fetchAllProducts(worksheetId);
}

export const deleteProduct = async function(worksheetId, productId) {
  await fetch(`product/${productId}/worksheet/${worksheetId}`, {method: "delete"});
  await this.fetchAllProducts(worksheetId);
}