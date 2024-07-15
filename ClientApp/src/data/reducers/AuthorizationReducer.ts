import {createAction, createReducer} from "@reduxjs/toolkit";
import {CurrentUser} from "../DataTypes";

export const LoginAction = createAction<CurrentUser>("auth/login");
export const LogoutAction = createAction("auth/logout");


const authReducer = createReducer<CurrentUser | null>(null, builder => {
  builder
      .addCase(LogoutAction, () => null)
      .addCase(LoginAction, (_, action) => action.payload)
});

export {authReducer}