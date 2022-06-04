import {Component} from "react";
import {NodeProduction} from "../nodes/NodeProduction";
import {NodeSpawn} from "../nodes/NodeSpawn";
import {NodeEnd} from "../nodes/NodeEnd";

export class Calculator extends Component {
  render() {
    return (
      <div>
        <h1>Calculator</h1>
        <NodeProduction></NodeProduction>
        <NodeSpawn></NodeSpawn>
        <NodeEnd></NodeEnd>
      </div>
    );
  }
}