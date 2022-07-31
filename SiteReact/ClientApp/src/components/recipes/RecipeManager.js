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
    this.unsubscribe = Store.subscribe(() => this.setState({recipes: Store.getState().recipe}));
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

  componentWillUnmount() {
    this.unsubscribe();
  }
}