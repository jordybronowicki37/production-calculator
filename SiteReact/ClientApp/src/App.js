import './custom.css'
import React, {Component} from 'react';
import {WorksheetOverview} from "./components/worksheets/WorksheetOverview";
import {Calculator} from "./components/calculator/Calculator";
import {Layout} from "./components/layout/Layout";
import {Route} from "react-router-dom";
import {HomePage} from "./pages/HomePage";

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={HomePage}/>
        <Route path='/worksheets' component={WorksheetOverview}/>
        <Route path='/calculator/:id' component={Calculator}/>
      </Layout>
    );
  }
}
