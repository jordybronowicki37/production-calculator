import Store from "../../dataStore/DataStore";
import {throwErrorNotification} from "../notification/NotificationThrower";

export const fetchAllRecipes = async function(worksheetId) {
  let response = await fetch(`worksheet/${worksheetId}/recipe`);
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  let data = await response.json();
  Store.dispatch({type:"recipes/set", payload:data});
}

export const createRecipe = async function(worksheetId, body) {
  let response = await fetch(`worksheet/${worksheetId}/recipe`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify(body),
  });
  if (!response.ok) {
    throwErrorNotification(response.statusText);
    return;
  }
  let json = await response.json();
  Store.dispatch({type:"recipes/add", payload:json});
  return json;
}
