import Store from "../../dataStore/DataStore";

export const fetchAllRecipes = async function(worksheetId) {
  let response = await fetch(`worksheet/${worksheetId}/recipe`);
  if (!response.ok) throw new Error();
  let data = await response.json();
  Store.dispatch({type:"recipes/set", payload:data});
}
