import {createReducer} from "@reduxjs/toolkit";

const projectsReducer = createReducer([], {
  "projects/set": (state, action) => [...action.payload],
});

export {projectsReducer};