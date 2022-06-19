import {ProductListItem} from "./ProductListItem";

export function ProductList(list) {
  return (
    <ul>
      {list.map(product => ProductListItem(product))}
    </ul>
  );
}