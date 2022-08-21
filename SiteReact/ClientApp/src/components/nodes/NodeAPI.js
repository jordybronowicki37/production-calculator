import Store from "../../dataStore/DataStore";

export const nodeCreate = async function(worksheetId, type) {
  let response = await fetch(`worksheet/${worksheetId}/node`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({type})
  });
  if (!response.ok) throw new Error();
  let json = await response.json();
  Store.dispatch({type:"node/add", payload:json});
  return json;
}

export const nodeEditProduct = async function(worksheetId, nodeId, product) {
  let response = await fetch(`worksheet/${worksheetId}/node/${nodeId}/product`, {
    method: "patch",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({product})
  });
  if (!response.ok) throw new Error();
  let json = await response.json();
  Store.dispatch({type:"node/set", payload:json});
  return json;
}

export const nodeEditRecipe = async function(worksheetId, nodeId, recipe) {
  let response = await fetch(`worksheet/${worksheetId}/node/${nodeId}/recipe`, {
    method: "patch",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({recipe})
  });
  if (!response.ok) throw new Error();
  let json = await response.json();
  Store.dispatch({type:"node/set", payload:json});
  return json;
}

