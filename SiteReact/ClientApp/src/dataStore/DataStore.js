import {combineReducers, configureStore, createReducer} from "@reduxjs/toolkit";

const productReducer = createReducer([], {
  "product/set": (state, action) => [...action.payload],
});

const recipeReducer = createReducer([], {
  "recipe/set": (state, action) => [...action.payload],
});

const rootReducer = combineReducers({
  product: productReducer,
  recipe: recipeReducer,
});

const Store = configureStore({reducer: rootReducer});
export default Store;