import {Component} from "react";
import "./RecipeManager.css";
import Store from "../../dataStore/DataStore";

export class RecipeManager extends Component {
  unsubscribe;
  
  constructor(props) {
    super(props);
    this.state = {
      recipes: [],
      worksheetId: props.worksheetId,
    };
    this.fetchAll();
    this.unsubscribe = Store.subscribe(() => this.setState({recipes: Store.getState().recipe}));
  }

  render() {
    return (
      <div>
        <ul className="recipes">
          {this.state.recipes.map(recipe => (
            <li key={recipe.name}>
              <div>{recipe.name}</div>
              <button className="recipe-remove-button" title="Remove recipe"><box-icon type='solid' name='minus-circle' color="#ff8080"></box-icon></button>
            </li>))}
        </ul>
      </div>
    );
  }

  fetchAll() {
    fetch(`recipe/worksheet/${this.state.worksheetId}`).then(response => response.json()).then(recipes => {
      Store.dispatch({type: "recipe/set", payload: recipes});
    });
  }

  componentWillUnmount() {
    this.unsubscribe();
  }
}