import {createReducer} from "@reduxjs/toolkit";

const nodeReducer = createReducer([], {
  "reset": () => [],
  "nodes/set": (state, action) => [...action.payload],
  "nodes/unset": () => [],
  "nodes/update": (state, action) => {
    return [...action.payload].map((v, i) => {
      v.position = state[i].position;
      return v;
    });
  },
  "node/set": (state, action) => {
    let index = state.findIndex(v => v.id = action.payload.id);
    state[index] = action.payload;
    return state;
  },
  "node/add": (state, action) => [...state, action.payload],
  "node/remove": (state, action) => state.filter(value => value.id !== action.payload),
  "node/change/position": (state, action) => {
    let node = state.find(value => value.id === action.payload.id);
    node.position = action.payload.position;
    return state;
  },
  "node/change/product": (state, action) => {
    let node = state.find(value => value.id === action.payload.id);
    node.product = action.payload.product;
    return state;
  },
  "node/change/recipe": (state, action) => {
    let node = state.find(value => value.id === action.payload.id);
    node.recipe = action.payload.recipe;
    return state;
  },
  "node/change/targets": (state, action) => {
    let node = state.find(value => value.id === action.payload.id);
    node.targets = action.payload.targets;
    return state;
  },
});

export default {nodeReducer}