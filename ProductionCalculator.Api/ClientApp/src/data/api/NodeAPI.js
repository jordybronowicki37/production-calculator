import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";

export async function nodeCreateProduct(worksheetId, type, position, product) {
  return await nodeCreate(worksheetId, JSON.stringify({type, position, product}));
}

export async function nodeCreateRecipe(worksheetId, type, position, recipe, machine) {
  return await nodeCreate(worksheetId, JSON.stringify({type, position, recipe, machine}));
}

async function nodeCreate(worksheetId, body) {
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
  Store.dispatch({type:"node/add", payload:json, worksheetId:worksheetId});
  return json;
}

export async function nodeEditPosition(worksheetId, nodeId, position) {
  let response = await fetch(`worksheet/${worksheetId}/node/${nodeId}/position`, {
    method: "put",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({ x: position.x, y: position.y})
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  return await response.json();
}

export async function nodeEditProduct(worksheetId, nodeId, product) {
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

export async function nodeEditRecipe(worksheetId, nodeId, recipe) {
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

export async function nodeRemove(worksheetId, nodeId) {
  let response = await fetch(`worksheet/${worksheetId}/node/${nodeId}`, {method: "delete"});
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  Store.dispatch({type:"node/remove", payload: nodeId, worksheetId:worksheetId});
}
