import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";

export const nodeCreateProduct = async function(type, position, product) {
  return await nodeCreate(position, JSON.stringify({type, product}));
}

export const nodeCreateRecipe = async function(type, position, recipe) {
  return await nodeCreate(position, JSON.stringify({type, recipe}));
}

async function nodeCreate(position, body) {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/node`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body,
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json();
  json.position = position;
  Store.dispatch({type:"node/add", payload:json});
  return json;
}

export const nodeEditProduct = async function(nodeId, product) {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/node/${nodeId}/product`, {
    method: "put",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({product})
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json();
  Store.dispatch({type:"node/change/product", payload: {id:json.id, product:json.product}});
  return json;
}

export const nodeEditRecipe = async function(nodeId, recipe) {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/node/${nodeId}/recipe`, {
    method: "put",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({recipe})
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json();
  Store.dispatch({type:"node/change/recipe", payload: {id:json.id, recipe:json.recipe}});
  return json;
}

export const nodeRemove = async function(nodeId) {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/node/${nodeId}`, {method: "delete"});
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  Store.dispatch({type:"node/remove", payload: nodeId});
}
