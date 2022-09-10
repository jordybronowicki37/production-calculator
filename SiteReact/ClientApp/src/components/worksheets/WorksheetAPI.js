import Store from "../../dataStore/DataStore";
import {throwErrorNotification} from "../notification/NotificationThrower";

export const fetchAllWorksheets = async function() {
  let response = await fetch(`worksheet`);
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  let json = await response.json();
  Store.dispatch({type:"worksheets/set", payload:json});
  return json;
}

export const fetchWorksheet = async function(worksheetId) {
  let response = await fetch(`worksheet/${worksheetId}`);
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  let json = await response.json();
  
  // TODO remove when position is saved on back-end
  json.nodes.map((value, index) => {
    value.position = {x: 200, y: index*150};
    return value;
  });
  
  Store.dispatch({type:"products/set", payload:json.products});
  Store.dispatch({type:"recipes/set", payload:json.recipes});
  Store.dispatch({type:"nodes/set", payload:json.nodes});
  Store.dispatch({type:"connections/set", payload:json.connections});
  
  delete json.products;
  delete json.recipes;
  delete json.nodes;
  delete json.connections;
  Store.dispatch({type:"worksheet/set", payload:json});
  
  return json;
}

export const createNewWorksheet = async function(name) {
  name = name.trim();
  let response = await fetch(`worksheet`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({name})
  });
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  fetchAllWorksheets();
  return await response.json();
}

export const calculate = async function() {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/calculate`, {method: "post"});
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  let json = await response.json();
  
  Store.dispatch({type:"nodes/update", payload:json.nodes});
  Store.dispatch({type:"connections/update", payload:json.connections});
  
  delete json.products;
  delete json.recipes;
  delete json.nodes;
  delete json.connections;
  Store.dispatch({type:"worksheet/set", payload:json});
  
  return json;
}
