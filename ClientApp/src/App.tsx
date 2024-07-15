import './custom.css'
import {Layout} from "./components/layout/Layout";
import {Route} from "react-router-dom";
import {HomePage} from "./pages/HomePage";
import {Provider} from "react-redux";
import Store from "./data/DataStore";
import {ProjectsPage} from "./pages/ProjectsPage";
import {EditorPage} from "./pages/EditorPage";
import {AuthorizationPage} from "./pages/AuthorizationPage";

export function App() {
  return (
    <Provider store={Store}>
      <Layout>
        <Route exact path='/' component={HomePage}/>
        <Route path='/auth' component={AuthorizationPage}/>
        <Route path='/projects' component={ProjectsPage}/>
        <Route path='/editor/:id' component={EditorPage}/>
      </Layout>
    </Provider>
  );
}
