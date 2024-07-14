import Store from "../DataStore";
import {LoginAction, LogoutAction} from "../reducers/AuthorizationReducer";
import {CurrentUser} from "../DataTypes";
import {useRef} from "react";
import {AxiosResponse} from "axios";
import axiosInstance from "./AxiosConfig";
import {useSendRequestBase} from "./RequestHooksBase";

export type LoginParams = {
  email: string,
  password: string
}

export type LoginResponse = {
  userId: string,
  userName: string,
  email: string,
}

export function useLogin() {
  const abortController = useRef(new AbortController());
  const loginUser = (params: LoginParams) => {
    return axiosInstance.post<LoginResponse>('account/login', params, {
      signal: abortController.current.signal,
      headers: {noAuth: true}
    });
  }
  const handleLoginResponse = (response: AxiosResponse<LoginResponse, any>) => {
    const authToken = response.headers["authorization"];
    const currentUser: CurrentUser = {...response.data, authToken};
    Store.dispatch(LoginAction(currentUser));
  }
  const {data, error, loading, triggerRequest} = useSendRequestBase<LoginParams, LoginResponse>({
    abortController: abortController.current,
    action: loginUser,
    handleResponse: handleLoginResponse
  });

  return {data, error, loading, loginUser: triggerRequest, abortController: abortController.current};
}

export type RegisterParams = {
  username: string
  email: string,
  password: string,
  password2: string,
}

export type RegisterResponse = {
  userId: string,
  userName: string,
  email: string,
}

export function useRegister() {
  const abortController = useRef(new AbortController());
  const registerUser = (params: RegisterParams) => {
    return axiosInstance.post<RegisterResponse>('account/register', params, {
      signal: abortController.current.signal,
      headers: {noAuth: true}
    });
  }
  const handleRegisterResponse = (response: AxiosResponse<RegisterResponse, any>) => {
    const authToken = response.headers["authorization"];
    const currentUser: CurrentUser = {...response.data, authToken};
    Store.dispatch(LoginAction(currentUser));
  }
  const {data, error, loading, triggerRequest} = useSendRequestBase<RegisterParams, RegisterResponse>({
    abortController: abortController.current,
    action: registerUser,
    handleResponse: handleRegisterResponse
  });

  return {data, error, loading, registerUser: triggerRequest, abortController: abortController.current};
}

export function useLogout() {
  const abortController = useRef(new AbortController());
  const logoutUser = () => {
    return axiosInstance.post('account/logout', undefined, {
      signal: abortController.current.signal,
    });
  }
  const handleLogoutResponse = () => {
    Store.dispatch(LogoutAction());
  }
  const {data, error, loading, triggerRequest} = useSendRequestBase<any, any>({
    abortController: abortController.current,
    action: logoutUser,
    handleResponse: handleLogoutResponse
  });

  return {data, error, loading, logoutUser: triggerRequest, abortController: abortController.current};
}