import Store from "./DataStore";
import {throwErrorNotification} from "../components/notification/NotificationThrower";

export const fetchAllProducts = async function() {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/product`);
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
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
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  await fetchAllProducts();
}

export const deleteProduct = async function(productId) {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/product/${productId}`, {method: "delete"});
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  await fetchAllProducts();
}