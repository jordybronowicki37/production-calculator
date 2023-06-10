import {Calculator} from "../../calculator/Calculator";

export function CalculatorTab({worksheet, products, recipes, machines}) {
  return <Calculator worksheet={worksheet} products={products} recipes={recipes} machines={machines}/>
}
