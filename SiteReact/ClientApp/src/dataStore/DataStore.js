import {combineReducers, configureStore, createReducer} from "@reduxjs/toolkit";

const productReducer = createReducer([], {
  "product/set": (state, action) => [...action.payload],
});

const recipeReducer = createReducer([], {
  "recipe/set": (state, action) => [...action.payload],
});

const worksheetsReducer = createReducer([], {
  "worksheets/set": (state, action) => [...action.payload],
});

const worksheetReducer = createReducer(null, {
  "worksheet/set": (state, action) => action.payload,
});

const nodeReducer = createReducer([], {
  "nodes/set": (state, action) => [...action.payload],
  "node/set": (state, action) => {
    let index = state.findIndex(v => v.id = action.payload.id);
    state[index] = action.payload;
    return state;
  },
  "node/add": (state, action) => {
    return [...state, action.payload];
  },
});

const rootReducer = combineReducers({
  product: productReducer,
  recipe: recipeReducer,
  worksheets: worksheetsReducer,
  worksheet: worksheetReducer,
  node: nodeReducer,
});

const Store = configureStore({reducer: rootReducer});
export default Store;