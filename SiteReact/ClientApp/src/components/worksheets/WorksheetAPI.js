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
  Store.dispatch({type:"worksheet/set", payload:json});
  Store.dispatch({type:"nodes/set", payload:json.nodes});
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
