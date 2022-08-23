import Store from "../../dataStore/DataStore";

export const fetchAllWorksheets = async function() {
  let response = await fetch(`worksheet`);
  if (!response.ok) throw new Error();
  let json = await response.json();
  Store.dispatch({type:"worksheets/set", payload:json});
  return json;
}

export const fetchWorksheet = async function(worksheetId) {
  let response = await fetch(`worksheet/${worksheetId}`);
  if (!response.ok) throw new Error();
  let json = await response.json();
  
  // TODO remove when position is saved on back-end
  json.nodes.map((value, index) => {
    value.position = {x: 200, y: index*150};
    return value;
  });
  
  Store.dispatch({type:"worksheet/set", payload:json});
  Store.dispatch({type:"nodes/set", payload:json.nodes});
  Store.dispatch({type:"connections/set", payload:json.connections});
  Store.dispatch({type:"products/set", payload:json.products});
  Store.dispatch({type:"recipes/set", payload:json.recipes});
  return json;
}

export const createNewWorksheet = async function(name) {
  name = name.trim();
  let response = await fetch(`worksheet`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify({name})
  });
  if (!response.ok) throw new Error();
  fetchAllWorksheets();
  return await response.json();
}
