import "./Layout.css";
import {NavMenu} from './NavMenu';
import {UserNotificationManager} from "../notification/UserNotificationManager";

export function Layout({children}) {
    return (
        <div className="layout min-vh-100">
            <NavMenu />
            <div className="notification-wrapper">
                <UserNotificationManager/>
            </div>
            {children}
        </div>
    );
}
