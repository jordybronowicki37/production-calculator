import {createReducer} from "@reduxjs/toolkit";

const projectsReducer = createReducer([], {
  "projects/set": (state, action) => [...action.payload],
  "projects/add": (state, action) => [...state, action.payload],
});

export {projectsReducer};