import {combineReducers, configureStore, createReducer} from "@reduxjs/toolkit";
import notificationReducer from "./reducers/NotificationReducer";
import productReducer from "./reducers/ProductReducer";
import recipeReducer from "./reducers/RecipeReducer";
import worksheetReducer from "./reducers/WorksheetReducer";
import nodeReducer from "./reducers/NodeReducer";
import connectionReducer from "./reducers/ConnectionReducer";

const worksheetsReducer = createReducer([], {
  "worksheets/set": (state, action) => [...action.payload],
});

const rootReducer = combineReducers({
  products: productReducer,
  recipes: recipeReducer,
  worksheets: worksheetsReducer,
  worksheet: worksheetReducer,
  nodes: nodeReducer,
  connections: connectionReducer,
  notifications: notificationReducer,
});

const Store = configureStore({reducer: rootReducer});
export default Store;