import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {notificationReducer} from "./reducers/NotificationReducer";
import {productsReducer} from "./reducers/ProductsReducer";
import {recipesReducer} from "./reducers/RecipesReducer";
import {worksheetsReducer} from "./reducers/WorksheetsReducer";
import {projectsReducer} from "./reducers/ProjectsReducer";
import {projectReducer} from "./reducers/ProjectReducer";
import {machinesReducer} from "./reducers/MachinesReducer";

const rootReducer = combineReducers({
  projects: projectsReducer,
  project: projectReducer,
  worksheets: worksheetsReducer,
  products: productsReducer,
  recipes: recipesReducer,
  machines: machinesReducer,
  notifications: notificationReducer,
});

const Store = configureStore({reducer: rootReducer});
export default Store;