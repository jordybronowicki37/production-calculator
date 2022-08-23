import Store from "../../dataStore/DataStore";

export const fetchAllProducts = async function(worksheetId) {
  let response = await fetch(`worksheet/${worksheetId}/product`);
  if (!response.ok) throw new Error();
  let data = await response.json();
  Store.dispatch({type:"products/set", payload:data});
}

export const postNewProduct = async function(worksheetId, productName) {
  productName = productName.trim();
  if (productName === "") return;
  let response = await fetch(`worksheet/${worksheetId}/product`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({name: productName})
  });
  if (!response.ok) throw new Error();
  await fetchAllProducts(worksheetId);
}

export const deleteProduct = async function(worksheetId, productId) {
  let response = await fetch(`worksheet/${worksheetId}/product/${productId}`, {method: "delete"});
  if (!response.ok) throw new Error();
  await fetchAllProducts(worksheetId);
}