import {createReducer} from "@reduxjs/toolkit";

const productReducer = createReducer([], {
  "reset": () => [],
  "project/unload": () => [],
  "project/load": (state, action) => action.payload.products,
  "products/set": (state, action) => [...action.payload],
  "products/add": (state, action) => [...state, action.payload],
});

export {productReducer}