import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";

export const fetchAllProjects = async function() {
  let response = await fetch(`project`);
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let data = await response.json();
  Store.dispatch({type:"projects/set", payload:data});
}

export const fetchProject = async function(id) {
  let response = await fetch(`project/${id}`);
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let data = await response.json();
  Store.dispatch({type:"load_project", payload:data});
}
