import "./RecipeItem.scss";
import {Machine, Product, Recipe, ThroughPut} from "../../../data/DataTypes";
import {RoundedAmountField} from "../../misc/RoundedAmountField";
import React from "react";

export type RecipeItemProps = {
  recipe: Recipe,
  products: Product[],
  machine: Machine | undefined,
}

export function RecipeItem({recipe, products, machine}: RecipeItemProps): React.JSX.Element {
  return (
    <div className="recipe-item">
      <div className="recipe-content">
        <div>Name:</div>
        <div>{recipe.name}</div>
        {
          machine !== undefined && <>
            <div>Machine:</div>
            <div>{machine.name}</div>
          </>
        }
        <div>
          <div>Inputs</div>
          {generateThroughputList(products, recipe.inputThroughPuts)}
        </div>
        <div>
          <div>Outputs</div>
          {generateThroughputList(products, recipe.outputThroughPuts)}
        </div>
      </div>
    </div>
  );
}

function generateThroughputList(products: Product[], throughPuts: ThroughPut[]) {
  return throughPuts.map(v => {
    const product = findProduct(products, v.product);
    if (product == undefined) return <div>Unknown product</div>
    return <div key={v.product} className="throughput">
      <div>{product.name}:</div>
      <RoundedAmountField amount={v.amount}/>
    </div>
    }
  );
}

function findProduct(products: Product[], id: string): Product | undefined {
  return products.find(v => v.id === id);
}
