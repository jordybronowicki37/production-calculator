import 'bootstrap/dist/css/bootstrap.css';
import ReactDOM from 'react-dom/client'
import {HashRouter} from 'react-router-dom';
import {App} from './App';
import 'material-symbols';
import 'material-icons/iconfont/material-icons.css';

const rootElement = document.getElementById('root');
if (!rootElement) throw new Error('Could not find root element');

ReactDOM.createRoot(rootElement).render(
  <HashRouter>
    <App />
  </HashRouter>);
