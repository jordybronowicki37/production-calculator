import {Node} from "./Node";
import './Node.css';
import './NodeEnd.css';

export class NodeEnd extends Node {
  render () {
    return (
      <div className="node-container">
        <div className="node-top">
          <h3>End</h3>
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