import {createReducer} from "@reduxjs/toolkit";
import {Machine} from "../DataTypes";
import {ResetAction} from "./GlobalActions";
import {ProjectLoadAction, ProjectUnloadAction} from "./ProjectReducer";

const machinesReducer = createReducer<Machine[]>([], builder => {
  builder
      .addCase(ResetAction, () => [])
      .addCase(ProjectUnloadAction, () => [])
      .addCase(ProjectLoadAction, (state, action) => action.payload.machines);
});

export {machinesReducer}