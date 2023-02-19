import {createReducer} from "@reduxjs/toolkit";

const productReducer = createReducer([], {
  "reset": () => [],
  "products/set": (state, action) => [...action.payload],
  "products/add": (state, action) => [...state, action.payload],
});

export {productReducer}