import {Node} from "./Node";
import './NodeSpawn.css';

export class NodeSpawn extends Node {
  constructor(props) {
    super(props);
  }

  render () {
    let productField = <div className="previewField">name</div>;
    
    if (!super.previewMode()) {
      productField =
        <select value={super.product()} onChange={e => this.productChanged(e.target.value)}>
          {super.products().map(v => (
            <option key={v.name} value={v.name}>{v.name}</option>))}
        </select>
    }
    
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
        <div className="node-content node-table">
          <div>Product</div>
          {productField}
          <div>Amount</div>
          <div className={super.previewMode()?"previewField":""}>{super.amount()}</div>
        </div>
      </div>
    );
  }
  
  productChanged(name) {
    console.log(name)
    // TODO change actual value
  }
}