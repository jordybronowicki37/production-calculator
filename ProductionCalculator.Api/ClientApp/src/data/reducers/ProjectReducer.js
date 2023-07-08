import {createReducer} from "@reduxjs/toolkit";

const projectReducer = createReducer(null, {
  "reset": () => null,
  "project/unload": () => null,
  "project/load": (state, action) => {
    const {products, recipes, machines, worksheets, ...other} = action.payload;
    return other;
  },
});

export {projectReducer};