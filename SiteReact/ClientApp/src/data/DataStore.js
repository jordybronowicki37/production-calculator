import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {notificationReducer} from "./reducers/NotificationReducer";
import {productReducer} from "./reducers/ProductReducer";
import {recipeReducer} from "./reducers/RecipeReducer";
import {worksheetReducer} from "./reducers/WorksheetReducer";
import {nodeReducer} from "./reducers/NodeReducer";
import {connectionReducer} from "./reducers/ConnectionReducer";
import {projectsReducer} from "./reducers/ProjectsReducer";
import {projectReducer} from "./reducers/ProjectReducer";

const rootReducer = combineReducers({
  projects: projectsReducer,
  project: projectReducer,
  worksheet: worksheetReducer,
  products: productReducer,
  recipes: recipeReducer,
  nodes: nodeReducer,
  connections: connectionReducer,
  notifications: notificationReducer,
});

const Store = configureStore({reducer: rootReducer});
export default Store;