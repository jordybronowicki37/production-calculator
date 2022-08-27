import "./RecipeCreator.css";
import {Component} from "react";
import Store from "../../dataStore/DataStore";
import {createRecipe} from "./RecipeAPI";

export class RecipeCreator extends Component {
  unsubscribe;
  listIdGenerator=0;
  
  constructor(props) {
    super(props);
    
    let mode = props.mode;
    if (!mode) mode = "create";
    
    this.state = {
      mode,
      name:"",
      inputThroughPuts:[{product:"", amount:0, tempId:this.listIdGenerator++}],
      outputThroughPuts:[{product:"", amount:0, tempId:this.listIdGenerator++}],
      products: Store.getState().products,
    }
    
    this.unsubscribe = Store.subscribe(() => {
      this.setState({products:Store.getState().products});
    });
  }

  render() {
    return <div className="recipe-creator">
      <h3>Recipe creator</h3>
      <div>
        <input type="text" name="recipeName" placeholder="Recipe name" autoComplete="off" value={this.state.name} onChange={e => this.nameChanged(e.target.value)}/>
      </div>
      <div className="recipe-creator-separation-line"></div>
      <div className="recipe-creator-header">
        <div>Inputs</div>
        <button className="add-throughput-button" type="button" title="Add input throughput" onClick={() => this.inputThroughPutAdd()}>
          <i className='bx bxs-add-to-queue' style={{color:"#96f378"}}/>
        </button>
      </div>
      <div className="input-throughputs throughput-list">
        {this.state.inputThroughPuts.map((value, index) =>
          <div key={value.tempId}>
            <select className="throughput-product-field" value={value.product} onChange={e => this.inputThroughPutProductChanged(index, e.target.value)}>
              <option value="" disabled hidden></option>
              {this.state.products.map(v => (
                <option key={v.name} value={v.name}>{v.name}</option>))}
            </select>
            <input className="throughput-amount-field" type="number" min="0" value={value.amount} onChange={e => this.inputThroughPutAmountChanged(index, e.target.value)}/>
            <button className="remove-throughput-button" type="button" title="Remove throughput" onClick={e => this.inputThroughPutRemove(index)}>
              <i className='bx bxs-minus-circle' style={{color:"#ff8080"}}/>
            </button>
          </div>)}
      </div>
      <div className="recipe-creator-separation-line"></div>
      <div className="recipe-creator-header">
        <div>Outputs</div>
        <button className="add-throughput-button" type="button" title="Add output throughput" onClick={() => this.outputThroughPutAdd()}>
          <i className='bx bxs-add-to-queue' style={{color:"#96f378"}}/>
        </button>
      </div>
      <div className="output-throughputs throughput-list">
        {this.state.outputThroughPuts.map((value, index) =>
          <div key={value.tempId}>
            <select className="throughput-product-field" value={value.product} onChange={e => this.outputThroughPutProductChanged(index, e.target.value)}>
              <option value="" disabled hidden></option>
              {this.state.products.map(v => (
                <option key={v.name} value={v.name}>{v.name}</option>))}
            </select>
            <input className="throughput-amount-field" type="number" min="0" value={value.amount} onChange={e => this.outputThroughPutAmountChanged(index, e.target.value)}/>
            <button className="remove-throughput-button" type="button" title="Remove throughput" onClick={e => this.outputThroughPutRemove(index)}>
              <i className='bx bxs-minus-circle' style={{color:"#ff8080"}}/>
            </button>
          </div>)}
      </div>
      <div className="recipe-creator-separation-line"></div>
      <button className="recipe-creator-submit" type="submit" onClick={e => this.createRecipe(e)}>Create</button>
    </div>
  }
  
  nameChanged(value) {
    this.setState({name:value});
  }

  inputThroughPutAdd() {
    this.setState({inputThroughPuts:[...this.state.inputThroughPuts, {product:"", amount:0, tempId:this.listIdGenerator++}]});
  }
  
  inputThroughPutRemove(index) {
    this.setState({inputThroughPuts:this.state.inputThroughPuts.filter((_, i) => i!==index)});
  }
  
  inputThroughPutProductChanged(index, value) {
    let list = [...this.state.inputThroughPuts]
    list[index].product=value;
    this.setState({inputThroughPuts:list});
  }
  
  inputThroughPutAmountChanged(index, value) {
    let list = [...this.state.inputThroughPuts]
    list[index].amount=value;
    this.setState({inputThroughPuts:list});
  }

  outputThroughPutAdd() {
    this.setState({outputThroughPuts:[...this.state.outputThroughPuts, {product:"", amount:0, tempId:this.listIdGenerator++}]});
  }
  
  outputThroughPutRemove(index) {
    this.setState({outputThroughPuts:this.state.outputThroughPuts.filter((_, i) => i!==index)});
  }
  
  outputThroughPutProductChanged(index, value) {
    let list = [...this.state.outputThroughPuts]
    list[index].product=value;
    this.setState({outputThroughPuts:list});
  }
  
  outputThroughPutAmountChanged(index, value) {
    let list = [...this.state.outputThroughPuts]
    list[index].amount=value;
    this.setState({outputThroughPuts:list});
  }
  
  createRecipe(event) {
    event.preventDefault();
    let dto = {
      name: this.state.name,
      inputThroughPuts: this.state.inputThroughPuts.map(v => {return {amount: parseInt(v.amount), product: {name: v.product}}}),
      outputThroughPuts: this.state.outputThroughPuts.map(v => {return {amount: parseInt(v.amount), product: {name: v.product}}}),
    };
    createRecipe(Store.getState().worksheet.id, dto)
  }

  componentWillUnmount() {
    this.unsubscribe();
  }
}