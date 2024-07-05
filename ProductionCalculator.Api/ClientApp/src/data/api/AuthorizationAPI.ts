import {throwErrorNotification} from "../../components/notification/NotificationThrower";
import Store from "../DataStore";
import {LoginAction} from "../reducers/AuthorizationReducer";
import {CurrentUser} from "../DataTypes";

export async function login(email: string, password: string): Promise<CurrentUser> {
    let response = await fetch(`account/login`, {
        method: "post",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            email,
            password
        })
    });
    if (!response.ok) {
        let error = await response.text();
        throwErrorNotification(error);
        throw new Error(error);
    }
    let json = await response.json() as CurrentUser;
    json.authToken = response.headers.get("Authorization");
    Store.dispatch(LoginAction(json));
    return json;
}

export async function register(username: string, email: string, password: string): Promise<CurrentUser> {
    let response = await fetch(`account/register`, {
        method: "post",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({
            username,
            email,
            password
        })
    });
    if (!response.ok) {
        let error = await response.text();
        throwErrorNotification(error);
        throw new Error(error);
    }
    let json = await response.json() as CurrentUser;
    json.authToken = response.headers.get("Authorization");
    Store.dispatch(LoginAction(json));
    return json;
}

export async function logout(): Promise<CurrentUser> {
    const auth = Store.getState().auth;
    if (auth == null) return Promise.reject();
    let response = await fetch(`account/logout`, {
        method: "post",
        headers: {"Content-Type": "application/json", "Authorization": auth.authToken},
        body: ""
    });
    if (!response.ok) {
        let error = await response.text();
        throwErrorNotification(error);
        throw new Error(error);
    }
    let json = await response.json() as CurrentUser;
    json.authToken = response.headers.get("Authorization");
    Store.dispatch(LoginAction(json));
    return json;
}