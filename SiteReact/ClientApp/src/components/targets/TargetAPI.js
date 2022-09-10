import Store from "../../dataStore/DataStore";
import {throwErrorNotification} from "../notification/NotificationThrower";

export const setTargets = async function(nodeId, targets) {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/node/${nodeId}/targets`, {
    method: "put",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify(targets),
  });
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  let data = await response.json();
  Store.dispatch({type:"node/change/targets", payload:data});
}