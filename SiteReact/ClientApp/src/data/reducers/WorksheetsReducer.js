import {createReducer} from "@reduxjs/toolkit";

const worksheetsReducer = createReducer([], {
  "reset": () => null,
  "unload_project": () => null,
  "load_project": (state, action) => {
    const { worksheets } = action.payload;
    return worksheets;
  },
  "worksheet/create": (state, action) => {
    state.push(action.payload);
    return state;
  },
  "worksheet/calculate": (state, action) => {
    const worksheet = findWorksheet(state, action.worksheetId);
    worksheet.connections = action.payload.connections;
    worksheet.nodes = [...action.payload.nodes].map((v, i) => {
      v.position = worksheet.nodes[i].position;
      return v;
    });
    return state;
  },
  
  "connection/add": (state, action) => {
    const worksheet = findWorksheet(state, action.worksheetId);
    worksheet.connections.push(action.payload);
    return state;
  },
  "connection/remove": (state, action) => {
    const worksheet = findWorksheet(state, action.worksheetId);
    worksheet.connections = worksheet.connections.filter(v => v.id !== action.payload);
    return state;
  },
  
  "node/set": (state, action) => {
    const worksheet = findWorksheet(state, action.worksheetId);
    let index = findNodeIndex(worksheet.nodes, action.payload.id);
    worksheet.nodes[index] = action.payload;
    return state;
  },
  "node/add": (state, action) => {
    const worksheet = findWorksheet(state, action.worksheetId);
    worksheet.nodes.push(action.payload);
    return state;
  },
  "node/remove": (state, action) => {
    const worksheet = findWorksheet(state, action.worksheetId);
    worksheet.nodes = worksheet.nodes.filter(value => value.id !== action.payload);
    return state;
  },
  "node/change/position": (state, action) => {
    const worksheet = findWorksheet(state, action.worksheetId);
    let index = findNodeIndex(worksheet.nodes, action.payload.id);
    worksheet.nodes[index].position = action.payload.position;
    return state;
  },
  "node/change/product": (state, action) => {
    const worksheet = findWorksheet(state, action.worksheetId);
    let index = findNodeIndex(worksheet.nodes, action.payload.id);
    worksheet.nodes[index].product = action.payload.product;
    return state;
  },
  "node/change/recipe": (state, action) => {
    const worksheet = findWorksheet(state, action.worksheetId);
    let index = findNodeIndex(worksheet.nodes, action.payload.id);
    worksheet.nodes[index].recipe = action.payload.recipe;
    return state;
  },
  "node/change/targets": (state, action) => {
    const worksheet = findWorksheet(state, action.worksheetId);
    let index = findNodeIndex(worksheet.nodes, action.payload.id);
    worksheet.nodes[index].targets = action.payload.targets;
    return state;
  },
});

function findNodeIndex(nodes, id) {
  return nodes.findIndex(value => value.id === id);
}

function findWorksheet(state, id) {
  return state.find(w => w.id === id);
}

function findWorksheetIndex(state, id) {
  return state.findIndex(w => w.id === id);
}

export {worksheetsReducer}