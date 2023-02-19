import {createReducer} from "@reduxjs/toolkit";

const recipeReducer = createReducer([], {
  "reset": () => [],
  "recipes/set": (state, action) => [...action.payload],
  "recipes/add": (state, action) => [...state, action.payload],
});

export default {recipeReducer}