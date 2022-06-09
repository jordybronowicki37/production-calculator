import {Node} from "./Node";
import './NodeProduction.css';

export class NodeProduction extends Node {
  constructor(props) {
    super(props);
  }
  
  render () {
    let generateProductList = (list) => {
      return (
        <div className="node-list">
          {list.map((value, index) => {
            return (
              <div key={index}>
                <div style={{marginRight: 3+"px"}}>{value.product}: </div>
                <div>{value.amount}</div>
              </div>
            );
          })}
        </div>
      );
    };
    
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
        <div className="node-content node-product-table">
          <div>Recipe: </div>
          <div style={{borderLeft: "unset"}}>{super.recipe()}</div>
          <div>Amount: </div>
          <div style={{borderLeft: "unset"}}>{super.amount()}</div>
          
          <div className="node-table">
            <div>Required in</div>
            <div>Required out</div>
            {generateProductList(super.requiredInProducts())}
            {generateProductList(super.requiredOutProducts())}
          </div>
        </div>
      </div>
    );
  }
}