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
    };
    this.unsubscribe = Store.subscribe(() => this.setState({recipes: this.sortRecipes(Store.getState().recipes)}));
  }

  render() {
    return (
      <div>
        <ul className="recipes">
          {this.state.recipes.map(recipe => (
            <li key={recipe.name}>
              <div>{recipe.name}</div>
              <button className="recipe-remove-button" title="Remove recipe">
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