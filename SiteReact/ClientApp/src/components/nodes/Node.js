import React, { Component } from 'react';
import './Node.css';
import Store from "../../dataStore/DataStore";

/**
 * Handles connection management and basic data values for all nodes.
 * Nodes that extend this must have render methods
 */
export class Node extends Component {
  unsubscribe;
  
  constructor(props) {
    super(props);
    let data = props.data
    if (!data) data = {};
    
    let previewMode = false;
    if (props.previewMode === true) previewMode = true;
    
    this.state = {
      data: data,
      products: Store.getState().product,
      recipes: Store.getState().recipe,
      previewMode,
      targetEditorOpen: false,
    };
    
    this.unsubscribe = Store.subscribe(() => {
      this.setState({
        products: Store.getState().product,
        recipes: Store.getState().recipe
      });
    });
  }
  
  previewMode() {return this.state.previewMode;}
  
  product() {
    if (this.state.data.hasOwnProperty("product")) return this.state.data.product.name;
    return "";
  }
  
  products() {return this.state.products;}
  
  recipe() {
    if (this.state.data.hasOwnProperty("recipe")) return this.state.data.recipe.name;
    return "";
  }
  
  recipes() {return this.state.recipes;}
  
  amount() {
    if (this.state.data.hasOwnProperty("amount")) return this.state.data.amount;
    return 0;
  }
  
  requiredInProducts() {
    let d = this.state.data;
    if (!(d.hasOwnProperty("recipe") && d.hasOwnProperty("amount"))) return [];
    return d.recipe.inputThroughPuts.map(value => this.generateThroughputObj(value, d, this.actualInProducts()));
  }
  
  requiredOutProducts() {
    let d = this.state.data;
    if (!(d.hasOwnProperty("recipe") && d.hasOwnProperty("amount"))) return [];
    return d.recipe.outputThroughPuts.map(value => this.generateThroughputObj(value, d, this.actualOutProducts()));
  }
  
  generateThroughputObj(throughput, node, actualProducts) {
    let product = throughput.product.name;
    let totalRequired = throughput.amount * node.amount;
    let acquired = false;
    
    if (actualProducts.hasOwnProperty(product) && actualProducts[product] === totalRequired) acquired = true;
    
    return {
      product: product,
      amount: totalRequired,
      amountAcquired: acquired
    };
  }
  
  actualInProducts() {
    let d = this.state.data;
    let products = {};
    if (d.hasOwnProperty("inputNodes")) {
      for (const connection of d.inputNodes) {
        if (products.hasOwnProperty(connection.product)) {
          products[connection.product] += connection.amount;
        } else {
          products[connection.product] = connection.amount;
        }
      }
    }
    return products;
  }
  
  actualOutProducts() {
    let d = this.state.data;
    let products = {};
    if (d.hasOwnProperty("outputNodes")) {
      for (const connection of d.outputNodes) {
        if (products.hasOwnProperty(connection.product)) {
          products[connection.product] += connection.amount;
        } else {
          products[connection.product] = connection.amount;
        }
      }
    }
    return products;
  }

  componentWillUnmount() {
    this.unsubscribe();
  }
}