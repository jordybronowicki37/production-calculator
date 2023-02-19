import {createReducer} from "@reduxjs/toolkit";

const worksheetReducer = createReducer(null, {
  "reset": () => null,
  "worksheet/set": (state, action) => action.payload,
  "worksheet/unset": () => null,
});

export default {worksheetReducer}