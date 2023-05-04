import {createReducer} from "@reduxjs/toolkit";

const machinesReducer = createReducer([], {
  "reset": () => [],
  "unload_project": () => [],
  "load_project": (state, action) => {
    return action.payload.machines;
  },
});

export {machinesReducer}