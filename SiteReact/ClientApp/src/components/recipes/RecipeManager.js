import {Component} from "react";
import "./RecipeManager.css";
import Store from "../../dataStore/DataStore";

export class RecipeManager extends Component {
  unsubscribe;
  
  constructor(props) {
    super(props);
    this.state = {
      recipes: this.sortRecipes(Store.getState().recipes),
      worksheetId: props.worksheetId,
      filter: "",
    };
    this.unsubscribe = Store.subscribe(() => this.setState({recipes: this.sortRecipes(Store.getState().recipes)}));
  }

  render() {
    let recipesFiltered = this.state.recipes.filter(v => {
      let filter = this.state.filter.toLowerCase();
      if (v.name.toLowerCase().includes(filter)) return true;
      for (const input of v.inputThroughPuts) if (input.product.name.toLowerCase().includes(filter)) return true;
      for (const output of v.outputThroughPuts) if (output.product.name.toLowerCase().includes(filter)) return true;
      return false;
    });
    
    return (
      <div className="recipe-manager">
        <h3>Recipes</h3>

        <div className="separation-line"></div>

        <form className="filter">
          <input placeholder="Filter recipe" type="text" onChange={e => this.setState({filter: e.target.value})}/>
        </form>
        
        <div className="separation-line"></div>
        
        <ul className="recipes">
          {recipesFiltered.map(recipe => (
            <li key={recipe.name}>
              <div>{recipe.name}</div>
              <button className="remove-button" title="Remove recipe">
                <i className='bx bxs-minus-circle' style={{color:"#ff8080"}}/>
              </button>
            </li>))}
        </ul>
      </div>
    );
  }

  sortRecipes(recipes) {
    return [...recipes].sort((v1, v2) => {
      const n1 = v1.name;
      const n2 = v2.name;
      if (n1 > n2) return 1;
      if (n1 < n2) return -1;
      return 0
    });
  }

  componentWillUnmount() {
    this.unsubscribe();
  }
}