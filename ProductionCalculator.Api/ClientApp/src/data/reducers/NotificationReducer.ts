import {createAction, createReducer} from "@reduxjs/toolkit";

let notificationId = 0;

type NotificationCreate = {
  type: NotificationTypes,
  message: string,
}

type NotificationTypes = ""

type Notification = {
  id: number,
  time: Date,
  type: NotificationTypes,
  message: string,
}

export const NotificationAddAction = createAction<NotificationCreate>("notification/add");

const notificationReducer = createReducer<Notification[]>([], builder => {
  builder.addCase(NotificationAddAction, (state, action) => {
    const notification: Notification = {
      id: notificationId++,
      time: new Date(),
      message: action.payload.message,
      type: action.payload.type,
    }
    return [...state, notification];
  });
});

export {notificationReducer};