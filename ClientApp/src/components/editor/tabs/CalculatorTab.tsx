import {Calculator} from "../../calculator/Calculator";
import {Machine, Product, Recipe, Worksheet} from "../../../data/DataTypes";
import React from "react";

export type CalculatorTabProps = {
  worksheet: Worksheet, 
  products: Product[], 
  recipes: Recipe[], 
  machines: Machine[],
}

export function CalculatorTab({worksheet, products, recipes, machines}: CalculatorTabProps): React.JSX.Element {
  return <Calculator worksheet={worksheet} products={products} recipes={recipes} machines={machines}/>
}
