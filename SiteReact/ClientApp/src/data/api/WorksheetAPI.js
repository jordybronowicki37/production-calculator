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

export const createNewWorksheet = async function(projectId, name) {
  name = name.trim();
  let response = await fetch(`project/${projectId}`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({name})
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json();
  Store.dispatch({type:"worksheet/create", payload:json});
  return json;
}

export const calculate = async function(worksheetId) {
  let response = await fetch(`worksheet/${worksheetId}/calculate`, {method: "post"});
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json();
  Store.dispatch({type:"worksheet/calculate", payload:json, worksheetId:worksheetId});
  return json;
}
