import Store from "../../dataStore/DataStore";
import {throwErrorNotification} from "../notification/NotificationThrower";

export const fetchAllProducts = async function() {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/product`);
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  let data = await response.json();
  Store.dispatch({type:"products/set", payload:data});
}

export const postNewProduct = async function(productName) {
  productName = productName.trim();
  if (productName === "") return;
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/product`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({name: productName})
  });
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  await fetchAllProducts();
}

export const deleteProduct = async function(productId) {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/product/${productId}`, {method: "delete"});
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  await fetchAllProducts();
}