import "./UserNotificationManager.scss";
import {Component} from "react";
import Store from "../../data/DataStore";

export class UserNotificationManager extends Component {
  unsubscribe;
  
  constructor(props) {
    super(props);
    
    this.state = {
      notifications:[],
      lastTimeStamp:0,
    }
    
    this.unsubscribe = Store.subscribe(() => {
      let notifications = JSON.parse(JSON.stringify(Store.getState().notifications));
      let recentNotifications = notifications.filter(v => {
        if (v.time <= this.state.lastTimeStamp) return false;
        this.setState({lastTimeStamp: v.time});
        return true;
      });
      recentNotifications.forEach(v => {
        console.log(JSON.parse(JSON.stringify(v)))
        v.disappearing = false;
        setTimeout(() => {
          v.disappearing = true;
          this.forceUpdate();
          setTimeout(() => this.setState({notifications:this.state.notifications.filter(t => t.id !== v.id)}), 1000);
        }, 5000);
      });
      this.setState({notifications:[...this.state.notifications, ...recentNotifications]});
    });
  }

  render() {
    return <div className="user-notification-manager">
      {this.state.notifications.map(v => {
        return <div key={v.id} className={`${v.type} ${v.disappearing?"disappearing":""}`}>
            <div className="icon-wrapper">
              <div hidden={v.type!=="info"}><i className='bx bx-info-circle'></i></div>
              <div hidden={v.type!=="success"}><i className='bx bx-check'></i></div>
              <div hidden={v.type!=="warning"}><i className='bx bx-error'></i></div>
              <div hidden={v.type!=="error"}><i className='bx bx-error-circle'></i></div>
            </div>
            <div className="text-wrapper">{v.message}</div>
          </div>
      })}
    </div>
  }
  
  componentWillUnmount() {
    this.unsubscribe();
  }
}