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
  "node/add": (state, action) => [...state, action.payload],
  "node/change/position": (state, action) => {
    let node = state.find(value => value.id == action.payload.id);
    node.position = action.payload.position;
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
});

const rootReducer = combineReducers({
  product: productReducer,
  recipe: recipeReducer,
  worksheets: worksheetsReducer,
  worksheet: worksheetReducer,
  node: nodeReducer,
  connection: connectionReducer,
});

const Store = configureStore({reducer: rootReducer});
export default Store;