import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";

export async function connectionCreate(worksheetId, nodeInId, nodeOutId, product) {
  let response = await fetch(`worksheet/${worksheetId}/connection`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({
      inputNodeId:nodeInId, 
      outputNodeId:nodeOutId, 
      product,
    })
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json();
  Store.dispatch({type:"connection/add", payload:json, worksheetId:worksheetId});
  return json;
}

export async function connectionDelete(worksheetId, connectionId) {
  let response = await fetch(`worksheet/${worksheetId}/connection/${connectionId}`, {method: "delete"});
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  Store.dispatch({type:"connection/remove", payload:connectionId, worksheetId:worksheetId});
  return response;
}
