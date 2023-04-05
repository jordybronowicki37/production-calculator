import {createReducer} from "@reduxjs/toolkit";

const worksheetReducer = createReducer(null, {
  "reset": () => null,
  "unload_worksheet": () => null,
  "load_worksheet": (state, action) => {
    const {nodes, connections, ...other} = action.payload;
    return other;
  },
});

export {worksheetReducer}