import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";
import {RecipeDto} from "./ApiDtoTypes";
import {RecipesAddAction, RecipesSetAction} from "../reducers/RecipesReducer";

export async function fetchAllRecipes(entityContainerId: string): Promise<RecipeDto[]> {
  let response = await fetch(`worksheet/${entityContainerId}/recipe`);
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json() as RecipeDto[];
  Store.dispatch(RecipesSetAction(json));
  return json;
}

export async function createRecipe(entityContainerId: string, body: object): Promise<RecipeDto> {
  let response = await fetch(`worksheet/${entityContainerId}/recipe`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify(body),
  });
  if (!response.ok) {
    let error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  let json = await response.json() as RecipeDto;
  Store.dispatch(RecipesAddAction(json));
  return json;
}
