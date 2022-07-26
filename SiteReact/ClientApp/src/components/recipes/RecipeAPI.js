import Store from "../../dataStore/DataStore";

export const fetchAllRecipes = async function(worksheetId) {
  let response = await fetch(`recipe/worksheet/${worksheetId}`);
  let data = await response.json();
  Store.dispatch({type:"recipe/set", payload:data});
}
