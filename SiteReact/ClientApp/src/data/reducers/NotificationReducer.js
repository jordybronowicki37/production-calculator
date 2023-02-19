import {createReducer} from "@reduxjs/toolkit";

let notificationId = 0;
const notificationReducer = createReducer([], {
  "notification/add": (state, action) => {
    action.payload.time = new Date().valueOf();
    action.payload.id = notificationId++;
    return [...state, action.payload];
  },
});

export default {notificationReducer};