import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";

export const fetchAllRecipes = async function(entityContainerId) {
  let response = await fetch(`worksheet/${entityContainerId}/recipe`);
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let data = await response.json();
  Store.dispatch({type:"recipes/set", payload:data});
}

export const createRecipe = async function(entityContainerId, body) {
  let response = await fetch(`worksheet/${entityContainerId}/recipe`, {
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
