import {createAction, createReducer} from "@reduxjs/toolkit";
import {RecipeDto} from "../api/ApiDtoTypes";
import {ResetAction} from "./GlobalActions";
import {ProjectLoadAction, ProjectUnloadAction} from "./ProjectReducer";

export const RecipesSetAction = createAction<RecipeDto[]>("recipes/set");
export const RecipesAddAction = createAction<RecipeDto>("recipes/add");

const recipesReducer = createReducer<RecipeDto[]>([], builder => {
  builder
      .addCase(ResetAction, () => [])
      .addCase(ProjectUnloadAction, () => [])
      .addCase(ProjectLoadAction, (state, action) => action.payload.recipes)
      .addCase(RecipesSetAction, (state, action) => [...action.payload])
      .addCase(RecipesAddAction, (state, action) => [...state, action.payload]);
});

export {recipesReducer};