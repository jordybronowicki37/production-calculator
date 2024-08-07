﻿import {createAction, createReducer} from "@reduxjs/toolkit";
import {Worksheet, Node} from "../DataTypes";
import {ResetAction} from "./GlobalActions";
import {ProjectLoadAction, ProjectUnloadAction} from "./ProjectReducer";
import {ConnectionDto, NodeDto, WorksheetDto} from "../api/ApiDtoTypes";

export const WorksheetCreateAction = createAction<WorksheetDto>("worksheet/create");
export const WorksheetCalculateAction = createAction<WorksheetDto>("worksheet/calculate");

export const ConnectionAddAction = createAction<ConnectionAddActionProps>("connection/add");
type ConnectionAddActionProps = { connection: ConnectionDto, worksheetId: string }
export const ConnectionEditAction = createAction<ConnectionEditActionProps>("connection/edit");
type ConnectionEditActionProps = { id: string, worksheetId: string, productId: string }
export const ConnectionRemoveAction = createAction<ConnectionRemoveActionProps>("connection/remove");
type ConnectionRemoveActionProps = { id: string, worksheetId: string }

export const NodeAddAction = createAction<NodeAddActionProps>("node/add");
type NodeAddActionProps = { node: NodeDto, worksheetId: string }
export const NodeChangeAction = createAction<NodeChangeActionProps>("node/change");
type NodeChangeActionProps = { node: NodeDto, worksheetId: string }
export const NodeRemoveAction = createAction<NodeRemoveActionProps>("node/remove");
type NodeRemoveActionProps = { id: string, worksheetId: string }

const worksheetsReducer = createReducer<Worksheet[]>([], builder => {
  builder
      .addCase(ResetAction, () => [])
      .addCase(ProjectUnloadAction, () => [])
      .addCase(ProjectLoadAction, (_, action) => action.payload.worksheets)
      .addCase(WorksheetCreateAction, (state, action) => [...state, action.payload])
      .addCase(WorksheetCalculateAction, (state, action) => {
        const worksheet = findWorksheet(state, action.payload.id);
        if (!worksheet) return;
        worksheet.alerts = action.payload.alerts;
        worksheet.connections = action.payload.connections;
        worksheet.nodes = [...action.payload.nodes].map((v, i) => {
          v.position = worksheet.nodes[i].position;
          return v;
        });
        return state;
      })
      .addCase(ConnectionAddAction, (state, action) => {
        const worksheet = findWorksheet(state, action.payload.worksheetId);
        if (!worksheet) return;
        worksheet.connections.push(action.payload.connection);
        return state;
      })
      .addCase(ConnectionEditAction, (state, action) => {
        const worksheet = findWorksheet(state, action.payload.worksheetId);
        if (!worksheet) return;
        const connection = worksheet.connections.find(v => v.id === action.payload.id);
        if (!connection) {
          console.error(`Connection was not found during store update. WorksheetId: ${worksheet.id}, connectionId: ${action.payload.id}`);
          return;
        }
        connection.productId = action.payload.productId;
        return state;
      })
      .addCase(ConnectionRemoveAction, (state, action) => {
        const worksheet = findWorksheet(state, action.payload.worksheetId);
        if (!worksheet) return;
        worksheet.connections = worksheet.connections.filter(v => v.id !== action.payload.id);
        return state;
      })
      .addCase(NodeAddAction, (state, action) => {
        const worksheet = findWorksheet(state, action.payload.worksheetId);
        if (!worksheet) return;
        worksheet.nodes.push(action.payload.node);
        return state;
      })
      .addCase(NodeChangeAction, (state, action) => {
        const worksheet = findWorksheet(state, action.payload.worksheetId);
        if (!worksheet) return;
        const index = findNodeIndex(worksheet.nodes, action.payload.node.id);
        worksheet.nodes[index] = action.payload.node;
        return state;
      })
      .addCase(NodeRemoveAction, (state, action) => {
        const worksheet = findWorksheet(state, action.payload.worksheetId);
        if (!worksheet) return;
        worksheet.nodes = worksheet.nodes.filter(value => value.id !== action.payload.id);
        return state;
      })
});

function findNodeIndex(nodes: Node[], id: string) {
  return nodes.findIndex(value => value.id === id);
}

function findWorksheet(state: Worksheet[], id: string): Worksheet | undefined {
  const worksheet = state.find(w => w.id === id);
  if (!worksheet) printWorksheetNotFoundMessage(id);
  return worksheet;
}

function printWorksheetNotFoundMessage(worksheetId: string) {
  console.error(`Worksheet was not found during store update: ${worksheetId}`)
}

export {worksheetsReducer}