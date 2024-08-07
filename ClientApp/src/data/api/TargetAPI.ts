import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";
import {NodeDto, ProductionTargetCreateDto} from "./ApiDtoTypes";
import {NodeChangeAction} from "../reducers/WorksheetsReducer";

export async function setTargets(
    worksheetId: string, 
    nodeId: string, 
    targets: ProductionTargetCreateDto[]): Promise<NodeDto> {
  const response = await fetch(`api/worksheet/${worksheetId}/node/${nodeId}/targets`, {
    method: "put",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify(targets),
  });
  if (!response.ok) {
    const error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  const json = await response.json() as NodeDto;
  Store.dispatch(NodeChangeAction({node: json, worksheetId}));
  return json;
}