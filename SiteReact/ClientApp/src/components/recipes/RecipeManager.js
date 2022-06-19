import {Component} from "react";
import "./RecipeManager.css";

export class RecipeManager extends Component {
  constructor(props) {
    super(props);
    this.state = {
      recipes: [],
    };

    this.fetchAll();
  }

  render() {
    return (
      <div>
        <ul className="recipes">
          {this.state.recipes.map(recipe => (
            <li key={recipe.name}>
              {recipe.name}
            </li>))}
        </ul>
      </div>
    );
  }

  fetchAll() {
    fetch("recipe/worksheet/1").then(response => response.json()).then(recipes => {
      this.setState({recipes: recipes});
    });
  }
}