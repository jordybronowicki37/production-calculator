import {Calculator} from "../../calculator/Calculator";
import {Machine, Product, Recipe, Worksheet} from "../../../data/DataTypes";

export type CalculatorTabProps = {
  worksheet: Worksheet, 
  products: Product[], 
  recipes: Recipe[], 
  machines: Machine[],
}

export function CalculatorTab({worksheet, products, recipes, machines}: CalculatorTabProps) {
  return <Calculator worksheet={worksheet} products={products} recipes={recipes} machines={machines}/>
}
