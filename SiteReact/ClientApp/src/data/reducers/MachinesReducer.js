import {createReducer} from "@reduxjs/toolkit";

const machinesReducer = createReducer([], {
  "reset": () => [],
  "project/unload": () => [],
  "project/load": (state, action) => {
    return action.payload.machines;
  },
});

export {machinesReducer}