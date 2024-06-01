import {createAction, createReducer} from "@reduxjs/toolkit";
import {ResetAction} from "./GlobalActions";
import {ProjectDto} from "../api/ApiDtoTypes";

export const ProjectsSetAction = createAction<ProjectDto[]>("projects/set");
export const ProjectsAddAction = createAction<ProjectDto>("projects/add");

const projectsReducer = createReducer<ProjectDto[]>([], builder => {
  builder
      .addCase(ResetAction, () => [])
      .addCase(ProjectsSetAction, (_, action) => [...action.payload])
      .addCase(ProjectsAddAction, (state, action) => [...state, action.payload])
});

export {projectsReducer};