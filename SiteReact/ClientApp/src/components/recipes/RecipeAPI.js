import Store from "../../dataStore/DataStore";
import {throwErrorNotification} from "../notification/NotificationThrower";

export const fetchAllRecipes = async function() {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/recipe`);
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let data = await response.json();
  Store.dispatch({type:"recipes/set", payload:data});
}

export const createRecipe = async function(body) {
  let response = await fetch(`worksheet/${Store.getState().worksheet.id}/recipe`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify(body),
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json();
  Store.dispatch({type:"recipes/add", payload:json});
  return json;
}
