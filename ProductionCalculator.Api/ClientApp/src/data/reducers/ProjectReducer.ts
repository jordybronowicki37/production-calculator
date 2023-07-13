import {createReducer, createAction} from "@reduxjs/toolkit";
import {ProjectDto} from "../api/ApiDtoTypes";
import {ResetAction} from "./GlobalActions";
import {Project} from "../DataTypes";

export const ProjectLoadAction = createAction<ProjectDto>("project/load");
export const ProjectUnloadAction = createAction("project/unload");

const projectReducer = createReducer<Project | null>(null, builder => {
  builder
      .addCase(ResetAction, () => null)
      .addCase(ProjectUnloadAction, () => null)
      .addCase(ProjectLoadAction, (state, action) => {
        const {worksheets, entityContainer, ...other} = action.payload;
        return {...other, entityContainerId: entityContainer.id};
      });
});

export {projectReducer};