import {Node} from "./Node";
import './NodeEnd.css';
import {nodeEditProduct} from "./NodeAPI";
import Store from "../../dataStore/DataStore";
import {TargetManager} from "../targets/TargetManager";

export class NodeEnd extends Node {
  constructor(props) {
    super(props);
  }
  
  render () {
    let productField = <div className="previewField">name</div>;
    let targets = <div></div>;
    let targetEditor = <div></div>;

    if (!super.previewMode()) {
      productField =
        <select value={super.product()} onChange={e => this.productChanged(e.target.value)}>
          <option value="" disabled hidden></option>
          {super.products().map(v => (
            <option key={v.name} value={v.name}>{v.name}</option>))}
        </select>;
      let targetIcon = <div></div>;
      let targetData = this.state.data.targets;
      if (targetData.length !== 0) {
        if (targetData[0].type === "ExactAmount") {
          targetIcon = <div>
            <i className='bx bx-arrow-to-right right-one'></i>
            <i className='bx bx-arrow-to-left left-one'></i>
          </div>;
        } else if (targetData[0].type === "MinAmount" || targetData[0].type === "MaxAmount") {
          targetIcon = <div>
            <i className='bx bx-arrow-to-left right-one'></i>
            <i className='bx bx-arrow-to-right left-one'></i>
          </div>;
        }
      }
      targets = 
        <div className="targets" onClick={e => this.setState({targetEditorOpen: true})}>
          {targetIcon}
          <i className='bx bx-target-lock'></i>
        </div>;
      targetEditor = 
        <div className="targetEditor" hidden={!this.state.targetEditorOpen}>
          <button type="button" className="popup-close-button" onClick={() => this.setState({targetEditorOpen: false})}>
            <i className='bx bx-x'></i>
          </button>
          <TargetManager nodeId={this.state.data.id} targets={targetData}></TargetManager>
        </div>
    }
    
    return (
      <div className="node-container">
        <div className="node-top">
          <h3>End</h3>
          {targets}
        </div>
        <div className="node-content node-table">
          <div>Product</div>
          {productField}
          <div>Amount</div>
          <div className={super.previewMode()?"previewField":""}>{super.amount()}</div>
        </div>
        {targetEditor}
      </div>
    );
  }

  productChanged(name) {
    nodeEditProduct(Store.getState().worksheet.id, this.state.data.id, name);
  }

  componentWillUnmount() {
    this.unsubscribe();
  }
}