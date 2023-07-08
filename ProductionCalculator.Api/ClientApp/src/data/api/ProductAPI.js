import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";

export async function fetchAllProducts(entityContainerId) {
  let response = await fetch(`worksheet/${entityContainerId}/product`);
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let data = await response.json();
  Store.dispatch({type:"products/set", payload:data});
}

export async function postNewProduct(entityContainerId, productName) {
  productName = productName.trim();
  if (productName === "") return;
  let response = await fetch(`worksheet/${entityContainerId}/product`, {
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

export async function deleteProduct(entityContainerId, productId) {
  let response = await fetch(`worksheet/${entityContainerId}/product/${productId}`, {method: "delete"});
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  await fetchAllProducts();
}