import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";
import {ConnectionDto} from "./ApiDtoTypes";
import {ConnectionAddAction, ConnectionRemoveAction} from "../reducers/WorksheetsReducer";

export async function connectionCreate(
    worksheetId: string, 
    inputNodeId: string, 
    outputNodeId: string, 
    product: string): Promise<ConnectionDto> {
  let response = await fetch(`worksheet/${worksheetId}/connection`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({
      inputNodeId,
      outputNodeId,
      product,
    })
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json();
  Store.dispatch(ConnectionAddAction({connection:json, worksheetId:worksheetId}));
  return json;
}

export async function connectionEdit(worksheetId: string, connectionId: string, productId: string): Promise<void> {
  let response = await fetch(`worksheet/${worksheetId}/connection/${connectionId}`, 
      {
        method: "put",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
          productId
        })
      });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  Store.dispatch(ConnectionRemoveAction({id:connectionId, worksheetId:worksheetId}));
}

export async function connectionDelete(worksheetId: string, connectionId: string): Promise<void> {
  let response = await fetch(`worksheet/${worksheetId}/connection/${connectionId}`, {method: "delete"});
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  Store.dispatch(ConnectionRemoveAction({id:connectionId, worksheetId:worksheetId}));
}
