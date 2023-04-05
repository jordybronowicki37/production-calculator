import {createReducer} from "@reduxjs/toolkit";

const connectionReducer = createReducer([], {
  "reset": () => [],
  "unload_worksheet": () => [],
  "load_worksheet": (state, action) => action.payload.connections,
  "connection/set": (state, action) => {
    let index = state.findIndex(v => v.id = action.payload.id);
    state[index] = action.payload;
    return state;
  },
  "connection/add": (state, action) => [...state, action.payload],
  "connection/remove": (state, action) => state.filter(v => v.id !== action.payload),
});

export {connectionReducer}