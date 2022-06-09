import React, { Component } from 'react';
import './Node.css';

/**
 * Handles connection management and basic data values for all nodes.
 * Nodes that extend this must have render methods
 */
export class Node extends Component {
  constructor(props) {
    super(props);
    let data = props.data
    if (!data) data = {};
    this.state = {
      data: data,
    };
  }
  
  product() {
    if (this.state.data.hasOwnProperty("product")) return this.state.data.product.name
    return "Geen";
  }
  
  amount() {
    if (this.state.data.hasOwnProperty("amount")) return this.state.data.amount
    return 0;
  }
}