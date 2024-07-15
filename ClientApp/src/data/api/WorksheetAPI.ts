import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";
import {WorksheetDto} from "./ApiDtoTypes";
import {WorksheetCalculateAction, WorksheetCreateAction} from "../reducers/WorksheetsReducer";

export async function createNewWorksheet(projectId: string, name: string): Promise<WorksheetDto> {
  name = name.trim();
  let response = await fetch(`project/${projectId}`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({name})
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json();
  Store.dispatch(WorksheetCreateAction(json));
  return json;
}

export async function calculate(worksheetId: string): Promise<WorksheetDto> {
  let response = await fetch(`worksheet/${worksheetId}/calculate`, {method: "post"});
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json();
  Store.dispatch(WorksheetCalculateAction(json));
  return json;
}
