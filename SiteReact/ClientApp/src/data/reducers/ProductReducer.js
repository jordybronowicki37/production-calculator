import {createReducer} from "@reduxjs/toolkit";

const productReducer = createReducer([], {
  "reset": () => [],
  "unload_project": () => [],
  "load_project": (state, action) => action.payload.products,
  "products/set": (state, action) => [...action.payload],
  "products/add": (state, action) => [...state, action.payload],
});

export {productReducer}