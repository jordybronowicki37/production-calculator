import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {notificationReducer} from "./reducers/NotificationReducer";
import {productReducer} from "./reducers/ProductReducer";
import {recipeReducer} from "./reducers/RecipeReducer";
import {worksheetsReducer} from "./reducers/WorksheetsReducer";
import {projectsReducer} from "./reducers/ProjectsReducer";
import {projectReducer} from "./reducers/ProjectReducer";
import {machinesReducer} from "./reducers/MachinesReducer";

const rootReducer = combineReducers({
  projects: projectsReducer,
  project: projectReducer,
  worksheets: worksheetsReducer,
  products: productReducer,
  recipes: recipeReducer,
  machines: machinesReducer,
  notifications: notificationReducer,
});

const Store = configureStore({reducer: rootReducer});
export default Store;