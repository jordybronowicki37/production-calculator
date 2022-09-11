import "./TargetManager.css"
import {Component} from "react";
import {setTargets} from "./TargetAPI";

export class TargetManager extends Component {
  constructor(props) {
    super(props);
    if (props.nodeId === undefined) throw new Error();
    
    let targets = [];
    if (props.targets !== undefined) targets = props.targets;
    
    this.state = {
      targets,
      nodeId: props.nodeId,
      mode: "none",
      targetsChanged:true,
      exactAmount: 0,
      minAmount: 0,
      maxAmount: 0,
    };
  }
  
  componentDidMount() {
    this.state.targets.forEach(v => {
      switch (v.type) {
        case "ExactAmount":
          this.setMode("exact");
          this.setAmount(v.amount, "exact");
          break;
        case "MinAmount":
          this.setMode("min-max");
          this.setAmount(v.amount, "min");
          break;
        case "MaxAmount":
          this.setMode("min-max");
          this.setAmount(v.amount, "max");
          break;
      }
    });
    this.setState({targetsChanged:false});
  }

  render() {
    return <div className="target-editor">
      <div className="tabs">
        <button type="button" title="No target" onClick={() => this.setMode("none")} className={`${this.state.mode==="none"?"selected":""}`}>
          <i className='bx bx-x'></i>
        </button>
        <button type="button" title="Exact target" onClick={() => this.setMode("exact")} className={`${this.state.mode==="exact"?"selected":""}`}>
          <i className='bx bx-arrow-to-right right-one'></i>
          <i className='bx bx-arrow-to-left left-one'></i>
        </button>
        <button type="button" title="Min-max target" onClick={() => this.setMode("min-max")} className={`${this.state.mode==="min-max"?"selected":""}`}>
          <i className='bx bx-arrow-to-left right-one'></i>
          <i className='bx bx-arrow-to-right left-one'></i>
        </button>
        <div className="filler"></div>
        <button hidden={!this.state.targetsChanged} type="submit" title="Save targets" onClick={() => this.saveTargets()}>
          <i className='bx bx-save' style={{'color':'#24bddc'}}></i>
        </button>
      </div>
      <div hidden={this.state.mode!=="none"} className="tab-content">
        <div className="visualisation">
          <div className="visualisation-line">
            <div className="blue"></div>
          </div>
        </div>
        <div className="target-values">
          <div>No target set</div>
        </div>
      </div>
      <div hidden={this.state.mode!=="exact"} className="tab-content">
        <div className="visualisation">
          <div className="visualisation-line">
            <div className={`${this.state.exactAmount>0?"gray":"blue"}`}></div>
          </div>
          <div className="visualisation-icon">
            <i title="Exact" className={`bx bxs-location-plus bx-flip-vertical ${this.state.exactAmount>0?"blue":"gray"}`}></i>
          </div>
        </div>
        <div className="target-values">
          <input type="number" className="amount-field" min="0" step="0.001" onChange={e => {this.setAmount(e.target.value, "exact")}} value={this.state.exactAmount}/>
        </div>
      </div>
      <div hidden={this.state.mode!=="min-max"} className="tab-content">
        <div className="visualisation">
          <div className="visualisation-line">
            <div className={`${this.state.minAmount>0?"gray":"blue"}`}></div>
            <div className="blue"></div>
            <div className={`${this.state.maxAmount>0?"gray":"blue"}`}></div>
          </div>
          <div className="visualisation-icon">
            <i title="Minimum" className={`bx bxs-location-plus bx-flip-vertical ${this.state.minAmount>0?"red":"gray"}`}></i>
            <i title="Maximum" className={`bx bxs-location-plus bx-flip-vertical ${this.state.maxAmount>0?"green":"gray"}`}></i>
          </div>
        </div>
        <div className="target-values">
          <input type="number" className="amount-field" min="0" step="0.001" onChange={e => {this.setAmount(e.target.value, "min")}} value={this.state.minAmount}/>
          <input type="number" className="amount-field" min="0" step="0.001" onChange={e => {this.setAmount(e.target.value, "max")}} value={this.state.maxAmount}/>
        </div>
      </div>
    </div>;
  }
  
  setMode(mode) {
    switch (mode) {
      case "none":
        this.setAmount(0, "none");
        break;
      case "exact":
      case "min-max":
        break;
      default:
        return;
    }
    this.setState({mode, targetsChanged:true});
  }
  
  setAmount(amount, type) {
    switch (type) {
      case "none":
        this.setState({
          exactAmount: 0,
          minAmount: 0,
          maxAmount: 0
        });
        break;
      case "exact":
        this.setState({
          exactAmount: amount,
          minAmount: 0,
          maxAmount: 0
        });
        break;
      case "min":
        this.setState({
          exactAmount: 0,
          minAmount: amount
        });
        break;
      case "max":
        this.setState({
          exactAmount: 0,
          maxAmount: amount
        });
        break;
      default:
        return;
    }
    this.setState({targetsChanged:true});
  }
  
  saveTargets() {
    let targetList = [];
    
    if (this.state.exactAmount > 0) targetList.push({type:"ExactAmount", amount:parseFloat(this.state.exactAmount)});
    if (this.state.minAmount > 0) targetList.push({type:"MinAmount", amount:parseFloat(this.state.minAmount)});
    if (this.state.maxAmount > 0) targetList.push({type:"MaxAmount", amount:parseFloat(this.state.maxAmount)});

    setTargets(this.state.nodeId, targetList).then(r => {
      this.setState({targetsChanged:false});
    });
  }
}