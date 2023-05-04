import {createReducer} from "@reduxjs/toolkit";

const projectReducer = createReducer(null, {
  "reset": () => null,
  "unload_project": () => null,
  "load_project": (state, action) => {
    const {products, recipes, machines, ...other} = action.payload;
    return other;
  },
});

export {projectReducer};