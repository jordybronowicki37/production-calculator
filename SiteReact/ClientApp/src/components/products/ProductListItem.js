export function ProductListItem(product) {
  return (
    <li key={product.name}>
      {product.name}
    </li>
  );
}