import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";

export const fetchWorksheet = async function(worksheetId) {
  let response = await fetch(`worksheet/${worksheetId}`);
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json();
  
  // TODO remove when position is saved on back-end
  json.nodes.map((value, index) => {
    value.position = {x: 200, y: index*150};
    return value;
  });
  
  Store.dispatch({type:"load_worksheet", payload:json});
  
  return json;
}

export const createNewWorksheet = async function(name, dataPreset) {
  name = name.trim();
  let response = await fetch(`worksheet`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({name, dataPreset})
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  
  return await response.json();
}

export const calculate = async function() {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/calculate`, {method: "post"});
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json();
  
  Store.dispatch({type:"worksheet/calculate", payload:json});
  
  return json;
}
