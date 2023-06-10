import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";

export const nodeCreateProduct = async function(worksheetId, type, position, product) {
  return await nodeCreate(worksheetId, position, JSON.stringify({type, product}));
}

export const nodeCreateRecipe = async function(worksheetId, type, position, recipe, machine) {
  return await nodeCreate(worksheetId, position, JSON.stringify({type, recipe, machine}));
}

async function nodeCreate(worksheetId, position, body) {
  let response = await fetch(`worksheet/${worksheetId}/node`, {
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
  Store.dispatch({type:"node/add", payload:json, worksheetId:worksheetId});
  return json;
}

export const nodeEditProduct = async function(worksheetId, nodeId, product) {
  let response = await fetch(`worksheet/${worksheetId}/node/${nodeId}/product`, {
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
  Store.dispatch({type:"node/change/product", payload: {id:json.id, product:json.product}, worksheetId:worksheetId});
  return json;
}

export const nodeEditRecipe = async function(worksheetId, nodeId, recipe) {
  let response = await fetch(`worksheet/${worksheetId}/node/${nodeId}/recipe`, {
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
  Store.dispatch({type:"node/change/recipe", payload: {id:json.id, recipe:json.recipe}, worksheetId:worksheetId});
  return json;
}

export const nodeRemove = async function(worksheetId, nodeId) {
  let response = await fetch(`worksheet/${worksheetId}/node/${nodeId}`, {method: "delete"});
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  Store.dispatch({type:"node/remove", payload: nodeId, worksheetId:worksheetId});
}
