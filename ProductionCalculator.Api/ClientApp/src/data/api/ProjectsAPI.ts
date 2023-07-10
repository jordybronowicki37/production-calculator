import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";
import {ProjectsAddAction, ProjectsSetAction} from "../reducers/ProjectsReducer";
import {ProjectDto} from "./ApiDtoTypes";
import {ProjectLoadAction} from "../reducers/ProjectReducer";

export async function fetchAllProjects(): Promise<ProjectDto[]> {
  let response = await fetch(`project`);
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json() as ProjectDto[];
  Store.dispatch(ProjectsSetAction(json));
  return json;
}

export async function fetchProject(id: string): Promise<ProjectDto> {
  let response = await fetch(`project/${id}`);
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json() as ProjectDto;
  Store.dispatch(ProjectLoadAction(json));
  return json;
}

export async function createProject(projectName: string, projectType: string): Promise<ProjectDto> {
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
  let json = await response.json() as ProjectDto;
  Store.dispatch(ProjectsAddAction(json));
  Store.dispatch(ProjectLoadAction(json));
  return json;
}
