import './custom.css'
import {Layout} from "./components/layout/Layout";
import {Route} from "react-router-dom";
import {HomePage} from "./pages/HomePage";
import {Provider} from "react-redux";
import Store from "./data/DataStore";
import {ProjectsPage} from "./pages/ProjectsPage";
import {EditorPage} from "./pages/EditorPage";

export function App() {
  return (
      <Provider store={Store}>
        <Layout>
          <Route exact path='/' component={HomePage}/>
          <Route path='/projects' component={ProjectsPage}/>
          <Route path='/editor/:id' component={EditorPage}/>
        </Layout>
      </Provider>
  );
}
