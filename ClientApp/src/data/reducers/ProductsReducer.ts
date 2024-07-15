import {createAction, createReducer} from "@reduxjs/toolkit";
import {ProjectLoadAction, ProjectUnloadAction} from "./ProjectReducer";
import {ResetAction} from "./GlobalActions";
import {ProductDto} from "../api/ApiDtoTypes";

export const ProductsSetAction = createAction<ProductDto[]>("products/set");
export const ProductsAddAction = createAction<ProductDto>("products/add");

const productsReducer = createReducer<ProductDto[]>([], builder => {
  builder
      .addCase(ResetAction, () => [])
      .addCase(ProjectUnloadAction, () => [])
      .addCase(ProjectLoadAction, (_, action) => action.payload.entityContainer.products)
      .addCase(ProductsSetAction, (_, action) => [...action.payload])
      .addCase(ProductsAddAction, (state, action) => [...state, action.payload])
});

export {productsReducer};