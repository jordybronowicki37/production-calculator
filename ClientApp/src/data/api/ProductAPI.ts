import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";
import {ProductsSetAction} from "../reducers/ProductsReducer";
import {ProductDto} from "./ApiDtoTypes";

export async function fetchAllProducts(entityContainerId: string): Promise<ProductDto[]> {
  const response = await fetch(`api/worksheet/${entityContainerId}/product`);
  if (!response.ok) {
    const error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  const json = await response.json() as ProductDto[];
  Store.dispatch(ProductsSetAction(json));
  return json;
}

export async function postNewProduct(entityContainerId: string, productName: string): Promise<void> {
  productName = productName.trim();
  if (productName === "") return;
  const response = await fetch(`api/worksheet/${entityContainerId}/product`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({name: productName})
  });
  if (!response.ok) {
    const error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  await fetchAllProducts(entityContainerId);
}

export async function deleteProduct(entityContainerId: string, productId: string): Promise<void> {
  const response = await fetch(`api/worksheet/${entityContainerId}/product/${productId}`, {method: "delete"});
  if (!response.ok) {
    const error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  await fetchAllProducts(entityContainerId);
}