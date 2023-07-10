import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";
import {NodePosition, NodeTypes} from "../DataTypes";
import {NodeDto} from "./ApiDtoTypes";
import {NodeAddAction, NodeChangeAction, NodeRemoveAction} from "../reducers/WorksheetsReducer";

export async function nodeCreateProduct(
    worksheetId: string,
    type: NodeTypes,
    position: NodePosition,
    product: string): Promise<NodeDto> {
  return await nodeCreate(worksheetId, JSON.stringify({type, position, product}));
}

export async function nodeCreateRecipe(
    worksheetId: string,
    type: NodeTypes,
    position: NodePosition,
    recipe: string,
    machine: string): Promise<NodeDto> {
  return await nodeCreate(worksheetId, JSON.stringify({type, position, recipe, machine}));
}

async function nodeCreate(worksheetId: string, body: string): Promise<NodeDto> {
  let response = await fetch(`worksheet/${worksheetId}/node`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body,
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json() as NodeDto;
  Store.dispatch(NodeAddAction({node:json, worksheetId:worksheetId}));
  return json;
}

export async function nodeEditPosition(
    worksheetId: string, 
    nodeId: string, 
    position: NodePosition): Promise<NodeDto> {
  let response = await fetch(`worksheet/${worksheetId}/node/${nodeId}/position`, {
    method: "put",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({ x: position.x, y: position.y})
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json() as NodeDto;
  Store.dispatch(NodeChangeAction({node: json, worksheetId:worksheetId}));
  return json;
}

export async function nodeEditProduct(
    worksheetId: string,
    nodeId: string,
    product: string): Promise<NodeDto> {
  let response = await fetch(`worksheet/${worksheetId}/node/${nodeId}/product`, {
    method: "put",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({product})
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json() as NodeDto;
  Store.dispatch(NodeChangeAction({node: json, worksheetId:worksheetId}));
  return json;
}

export async function nodeEditRecipe(
    worksheetId: string, 
    nodeId: string, 
    recipe: string): Promise<NodeDto> {
  let response = await fetch(`worksheet/${worksheetId}/node/${nodeId}/recipe`, {
    method: "put",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({recipe})
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json() as NodeDto;
  Store.dispatch(NodeChangeAction({node: json, worksheetId:worksheetId}));
  return json;
}

export async function nodeRemove(worksheetId: string, nodeId: string): Promise<void> {
  let response = await fetch(`worksheet/${worksheetId}/node/${nodeId}`, {method: "delete"});
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  Store.dispatch(NodeRemoveAction({id: nodeId, worksheetId:worksheetId}));
}
