import {combineReducers, configureStore, createReducer} from "@reduxjs/toolkit";

const productReducer = createReducer([], {
  "products/set": (state, action) => [...action.payload],
});

const recipeReducer = createReducer([], {
  "recipes/set": (state, action) => [...action.payload],
});

const worksheetsReducer = createReducer([], {
  "worksheets/set": (state, action) => [...action.payload],
});

const worksheetReducer = createReducer(null, {
  "worksheet/set": (state, action) => action.payload,
});

const nodeReducer = createReducer([], {
  "nodes/set": (state, action) => [...action.payload],
  "nodes/update": (state, action) => {
    return [...action.payload].map((v, i) => {
      v.position = state[i].position;
      return v;
    });
  },
  "node/set": (state, action) => {
    let index = state.findIndex(v => v.id = action.payload.id);
    state[index] = action.payload;
    return state;
  },
  "node/add": (state, action) => [...state, action.payload],
  "node/change/position": (state, action) => {
    let node = state.find(value => value.id === action.payload.id);
    node.position = action.payload.position;
    return state;
  },
  "node/change/product": (state, action) => {
    let node = state.find(value => value.id === action.payload.id);
    node.product = action.payload.product;
    return state;
  },
  "node/change/recipe": (state, action) => {
    let node = state.find(value => value.id === action.payload.id);
    node.recipe = action.payload.recipe;
    return state;
  },
});

const connectionReducer = createReducer([], {
  "connections/set": (state, action) => [...action.payload],
  "connection/set": (state, action) => {
    let index = state.findIndex(v => v.id = action.payload.id);
    state[index] = action.payload;
    return state;
  },
  "connection/add": (state, action) => [...state, action.payload],
  "connection/remove": (state, action) => state.filter(v => v.id !== action.payload),
});

const rootReducer = combineReducers({
  products: productReducer,
  recipes: recipeReducer,
  worksheets: worksheetsReducer,
  worksheet: worksheetReducer,
  nodes: nodeReducer,
  connections: connectionReducer,
});

const Store = configureStore({reducer: rootReducer});
export default Store;