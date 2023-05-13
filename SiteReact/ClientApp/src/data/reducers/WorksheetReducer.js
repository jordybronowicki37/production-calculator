import {createReducer} from "@reduxjs/toolkit";

const worksheetReducer = createReducer(null, {
  "reset": () => null,
  "unload_worksheet": () => null,
  "load_worksheet": (state, action) => {
    return action.payload;
  },
  "worksheet/calculate": (state, action) => {
    let newNodes = [...action.payload.nodes].map((v, i) => {
      v.position = state.nodes[i].position;
      return v;
    });
    return {...action.payload, nodes:newNodes};
  },
  
  "connection/add": (state, action) => {
    state.connections.push(action.payload);
    return state;
  },
  "connection/remove": (state, action) => {
    state.connections = state.connections.filter(v => v.id !== action.payload);
    return state;
  },
  
  "node/set": (state, action) => {
    let index = state.nodes.findIndex(v => v.id = action.payload.id);
    state.nodes[index] = action.payload;
    return state;
  },
  "node/add": (state, action) => {
    state.nodes.push(action.payload);
    return state;
  },
  "node/remove": (state, action) => {
    state.nodes = state.nodes.filter(value => value.id !== action.payload);
    return state;
  },
  "node/change/position": (state, action) => {
    let index = state.nodes.findIndex(value => value.id === action.payload.id);
    state.nodes[index].position = action.payload.position;
    return state;
  },
  "node/change/product": (state, action) => {
    let index = state.nodes.findIndex(value => value.id === action.payload.id);
    state.nodes[index].product = action.payload.product;
    return state;
  },
  "node/change/recipe": (state, action) => {
    let index = state.nodes.findIndex(value => value.id === action.payload.id);
    state.nodes[index].recipe = action.payload.recipe;
    return state;
  },
  "node/change/targets": (state, action) => {
    let index = state.nodes.findIndex(value => value.id === action.payload.id);
    state.nodes[index].targets = action.payload.targets;
    return state;
  },
});

export {worksheetReducer}