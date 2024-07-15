import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";
import {ProductsSetAction} from "../reducers/ProductsReducer";
import {ProductDto} from "./ApiDtoTypes";

export async function fetchAllProducts(entityContainerId: string): Promise<ProductDto[]> {
  let response = await fetch(`worksheet/${entityContainerId}/product`);
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json() as ProductDto[];
  Store.dispatch(ProductsSetAction(json));
  return json;
}

export async function postNewProduct(entityContainerId: string, productName: string): Promise<void> {
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
  await fetchAllProducts(entityContainerId);
}

export async function deleteProduct(entityContainerId: string, productId: string): Promise<void> {
  let response = await fetch(`worksheet/${entityContainerId}/product/${productId}`, {method: "delete"});
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  await fetchAllProducts(entityContainerId);
}