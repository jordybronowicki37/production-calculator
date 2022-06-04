import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import {Redirect} from "react-router-dom";
import {WorksheetOverview} from "./components/worksheets/WorksheetOverview";
import {Calculator} from "./components/calculator/Calculator";

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home}>
          <Redirect to="/calculator"/>
        </Route>
        <Route path='/worksheets' component={WorksheetOverview}/>
        <Route path='/calculator' component={Calculator}/>
      </Layout>
    );
  }
}
