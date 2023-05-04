import './custom.css'
import React, {Component} from 'react';
import {Layout} from "./components/layout/Layout";
import {Route} from "react-router-dom";
import {HomePage} from "./pages/HomePage";
import {Provider} from "react-redux";
import Store from "./data/DataStore";
import {CalculatorPage} from "./pages/CalculatorPage";
import {ProjectsPage} from "./pages/ProjectsPage";
import {ProjectPage} from "./pages/ProjectPage";
import {EditorPage} from "./pages/EditorPage";

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Provider store={Store}>
        <Layout>
          <Route exact path='/' component={HomePage}/>
          <Route path='/projects' component={ProjectsPage}/>
          <Route path='/project/:id' component={ProjectPage}/>
          <Route path='/calculator/:id' component={CalculatorPage}/>
          <Route path='/editor/:id' component={EditorPage}/>
        </Layout>
      </Provider>
    );
  }
}
