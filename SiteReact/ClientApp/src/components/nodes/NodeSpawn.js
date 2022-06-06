import {Node} from "./Node";
import './Node.css';
import './NodeSpawn.css';

export class NodeSpawn extends Node {
  render () {
    return (
      <div className="node-container">
        <div className="node-top">
          <h3>Spawn</h3>
          <div className="targets">
            <div>a</div>
            <div>b</div>
            <div>c</div>
          </div>
        </div>
        <div className="node-content">
          <div>Product</div>
          <div>20</div>
        </div>
      </div>
    );
  }
}