import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {Notification, notificationReducer} from "./reducers/NotificationReducer";
import {productsReducer} from "./reducers/ProductsReducer";
import {recipesReducer} from "./reducers/RecipesReducer";
import {worksheetsReducer} from "./reducers/WorksheetsReducer";
import {projectsReducer} from "./reducers/ProjectsReducer";
import {projectReducer} from "./reducers/ProjectReducer";
import {machinesReducer} from "./reducers/MachinesReducer";
import {Machine, Product, Project, Recipe, Worksheet} from "./DataTypes";
import {ProjectDto} from "./api/ApiDtoTypes";

export type StoreStates = {
  machines: Machine[],
  notifications: Notification[]
  products: Product[],
  project: Project,
  projects: ProjectDto[],
  recipes: Recipe[],
  worksheets: Worksheet[],
}

const rootReducer = combineReducers({
  machines: machinesReducer,
  notifications: notificationReducer,
  products: productsReducer,
  project: projectReducer,
  projects: projectsReducer,
  recipes: recipesReducer,
  worksheets: worksheetsReducer,
});

const Store = configureStore({reducer: rootReducer});
export default Store;