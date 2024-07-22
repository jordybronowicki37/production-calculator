import Store from "../DataStore";
import {throwErrorNotification} from "../../components/notification/NotificationThrower";
import {RecipeDto} from "./ApiDtoTypes";
import {RecipesAddAction, RecipesSetAction} from "../reducers/RecipesReducer";

export async function fetchAllRecipes(entityContainerId: string): Promise<RecipeDto[]> {
  const response = await fetch(`api/worksheet/${entityContainerId}/recipe`);
  if (!response.ok) {
    const error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  const json = await response.json() as RecipeDto[];
  Store.dispatch(RecipesSetAction(json));
  return json;
}

export async function createRecipe(entityContainerId: string, body: Record<string, unknown>): Promise<RecipeDto> {
  const response = await fetch(`api/worksheet/${entityContainerId}/recipe`, {
    method: "post",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify(body),
  });
  if (!response.ok) {
    const error = await response.text();
    throwErrorNotification(error);
    throw new Error(error);
  }
  const json = await response.json() as RecipeDto;
  Store.dispatch(RecipesAddAction(json));
  return json;
}
