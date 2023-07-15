import {Calculator} from "../../calculator/Calculator";
import {Machine, Product, Recipe, Worksheet} from "../../../data/DataTypes";

export function CalculatorTab({worksheet, products, recipes, machines}: 
    {worksheet: Worksheet, products: Product[], recipes: Recipe[], machines: Machine[]}) {
  return <Calculator worksheet={worksheet} products={products} recipes={recipes} machines={machines}/>
}
