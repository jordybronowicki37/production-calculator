import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";

export async function fetchAllProjects() {
  let response = await fetch(`project`);
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let data = await response.json();
  Store.dispatch({type:"projects/set", payload:data});
}

export async function fetchProject(id) {
  let response = await fetch(`project/${id}`);
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let data = await response.json();
  Store.dispatch({type:"project/load", payload:data});
}

export async function createProject(projectName, projectType) {
  let response = await fetch(`project`, {
    headers: {"Content-Type": "application/json"},
    method: "post",
    body: JSON.stringify({
      name: projectName,
      dataPreset: projectType
    }),
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let data = await response.json();
  Store.dispatch({type:"projects/add", payload:data});
  Store.dispatch({type:"project/load", payload:data});
}
