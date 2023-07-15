import Store from "../../data/DataStore";
import {NotificationAddAction, NotificationTypes} from "../../data/reducers/NotificationReducer";

export function throwInfoNotification (message: string): void {
  throwNotification(message, "info")
}

export function throwSuccessNotification (message: string): void {
  throwNotification(message, "success")
}

export function throwWarningNotification (message: string): void {
  throwNotification(message, "warning")
}

export function throwErrorNotification (message: string): void {
  throwNotification(message, "error")
}

const throwNotification = function (message: string, type: NotificationTypes): void {
  Store.dispatch(NotificationAddAction({message, type}));
}
