import {Component} from "react";
import "./RecipeManager.css";

export class RecipeManager extends Component {
  constructor(props) {
    super(props);
    this.state = {
      recipes: [],
      worksheetId: props.worksheetId,
    };
    this.fetchAll();
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
      this.setState({recipes: recipes});
    });
  }
}