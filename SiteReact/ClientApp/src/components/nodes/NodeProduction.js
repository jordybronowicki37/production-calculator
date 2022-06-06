import {Node} from "./Node";
import './Node.css';
import './NodeProduction.css';

export class NodeProduction extends Node {
  render () {
    return (
      <div className="node-container">
        <div className="node-top">
          <h3>Production</h3>
          <div className="targets">
            <div>a</div>
            <div>b</div>
            <div>c</div>
          </div>
        </div>
        <div className="node-content">

        </div>
      </div>
    );
  }
}