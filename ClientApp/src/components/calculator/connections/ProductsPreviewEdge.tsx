import {BaseEdge, EdgeLabelRenderer, EdgeProps, getBezierPath} from 'reactflow';
import {Connection, Product} from "../../../data/DataTypes";
import "./ProductsPreviewEdge.scss";
import {RoundedAmountField} from "../../misc/RoundedAmountField";
import React from "react";

export type ProductsPreviewEdgeType = {
  connections: Connection[],
  products: Product[],
  onOpenEditor: (edgeId: string) => void,
}

export function ProductsPreviewEdge(props: EdgeProps<ProductsPreviewEdgeType>): React.JSX.Element {
  const {id, sourceX, sourceY, targetX, targetY, sourcePosition, targetPosition} = props;
  const bezierPath = getBezierPath({sourceX, sourceY, sourcePosition, targetX, targetY, targetPosition});
  let [edgePath, labelX] = bezierPath;
  const labelY = bezierPath[2];
  const data = props.data;
  if (data == undefined) return <div>Something went wrong</div>;

  const nodeInId = id.split(";")[0];
  const nodeOutId = id.split(";")[1];

  if (nodeInId === nodeOutId) {
    const radiusX = 100;
    const radiusY = (sourceY - targetY) * 0.6;
    edgePath = `M${sourceX - 5},${sourceY} A${radiusX},${radiusY} 0 1 0 ${targetX + 2},${targetY}`;
    labelX += radiusX * 2
  }

  return (
    <>
      <BaseEdge {...props} path={edgePath}/>
      <EdgeLabelRenderer>
        <div
          style={{transform: `translate(-50%, -50%) translate(${labelX}px,${labelY}px)`,}}
          className="products-preview-edge-label">
          <div className="products-preview">
            {data.connections.map(value => {
              const product = findById(value.productId, data.products);
              return <div key={value.id}>
                <div>{product != undefined ? product.name : "Unknown product"}</div>
                <RoundedAmountField amount={value.amount}/>
              </div>
            })}
          </div>
          <button title="Edit connections" onClick={() => data.onOpenEditor(id)}>
            <span className="material-icons-round">settings_input_composite</span>
          </button>
        </div>
      </EdgeLabelRenderer>
    </>
  );
}

function findById(id: string, list: Product[]): Product | undefined {
  return list.find(v => v.id === id);
}
