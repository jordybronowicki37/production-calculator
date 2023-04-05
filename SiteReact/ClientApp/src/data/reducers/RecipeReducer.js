import {createReducer} from "@reduxjs/toolkit";

const recipeReducer = createReducer([], {
  "reset": () => [],
  "load_project": (state, action) => action.payload.recipes,
  "recipes/set": (state, action) => [...action.payload],
  "recipes/add": (state, action) => [...state, action.payload],
});

export {recipeReducer}