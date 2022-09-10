import Store from "../../dataStore/DataStore";
import {throwErrorNotification} from "../notification/NotificationThrower";

export const connectionCreate = async function(worksheetId, nodeInId, nodeOutId, product) {
  let response = await fetch(`worksheet/${worksheetId}/node/connection`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({
      inputNodeId:nodeInId, 
      outputNodeId:nodeOutId, 
      product,
    })
  });
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  let json = await response.json();
  Store.dispatch({type:"connection/add", payload:json});
  return json;
}

export const connectionDelete = async function(worksheetId, connectionId) {
  let response = await fetch(`worksheet/${worksheetId}/node/connection/${connectionId}`, {method: "delete"});
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  Store.dispatch({type:"connection/remove", payload:connectionId});
  return response;
}
