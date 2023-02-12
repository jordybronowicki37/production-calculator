import {throwErrorNotification} from "../components/notification/NotificationThrower";
import Store from "./DataStore";

export const connectionCreate = async function(nodeInId, nodeOutId, product) {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/connection`, {
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
  Store.dispatch({type:"connection/add", payload:json});
  return json;
}

export const connectionDelete = async function(connectionId) {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/connection/${connectionId}`, {method: "delete"});
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  Store.dispatch({type:"connection/remove", payload:connectionId});
  return response;
}
