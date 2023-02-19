import {createReducer} from "@reduxjs/toolkit";

const connectionReducer = createReducer([], {
  "reset": () => [],
  "connections/set": (state, action) => [...action.payload],
  "connections/unset": () => [],
  "connection/set": (state, action) => {
    let index = state.findIndex(v => v.id = action.payload.id);
    state[index] = action.payload;
    return state;
  },
  "connection/add": (state, action) => [...state, action.payload],
  "connection/remove": (state, action) => state.filter(v => v.id !== action.payload),
});

export default {connectionReducer}