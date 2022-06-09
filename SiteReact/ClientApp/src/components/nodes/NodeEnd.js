import {Node} from "./Node";
import './NodeEnd.css';

export class NodeEnd extends Node {
  constructor(props) {
    super(props);
  }
  
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
        <div className="node-content table">
          <div>Product</div>
          <div>{super.product()}</div>
          <div>Amount</div>
          <div>{super.amount()}</div>
        </div>
      </div>
    );
  }
}