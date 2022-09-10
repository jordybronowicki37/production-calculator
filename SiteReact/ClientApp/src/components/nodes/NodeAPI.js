import Store from "../../dataStore/DataStore";
import {throwErrorNotification} from "../notification/NotificationThrower";

export const nodeCreateProduct = async function(worksheetId, type, position, product) {
  return await nodeCreate(worksheetId, position, JSON.stringify({type, product}));
}

export const nodeCreateRecipe = async function(worksheetId, type, position, recipe) {
  return await nodeCreate(worksheetId, position, JSON.stringify({type, recipe}));
}

async function nodeCreate(worksheetId, position, body) {
  let response = await fetch(`worksheet/${worksheetId}/node`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body,
  });
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  let json = await response.json();
  json.position = position;
  Store.dispatch({type:"node/add", payload:json});
  return json;
}

export const nodeEditProduct = async function(worksheetId, nodeId, product) {
  let response = await fetch(`worksheet/${worksheetId}/node/${nodeId}/product`, {
    method: "put",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({product})
  });
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  let json = await response.json();
  Store.dispatch({type:"node/change/product", payload: {id:json.id, product:json.product}});
  return json;
}

export const nodeEditRecipe = async function(worksheetId, nodeId, recipe) {
  let response = await fetch(`worksheet/${worksheetId}/node/${nodeId}/recipe`, {
    method: "put",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({recipe})
  });
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  let json = await response.json();
  Store.dispatch({type:"node/change/recipe", payload: {id:json.id, recipe:json.recipe}});
  return json;
}

export const nodeRemove = async function(worksheetId, nodeId) {
  let response = await fetch(`worksheet/${worksheetId}/node/${nodeId}`, {method: "delete"});
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  Store.dispatch({type:"node/remove", payload: parseInt(nodeId)});
}
