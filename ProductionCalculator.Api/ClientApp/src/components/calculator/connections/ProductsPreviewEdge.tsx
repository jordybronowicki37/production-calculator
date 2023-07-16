import { BaseEdge, EdgeLabelRenderer, EdgeProps, getBezierPath } from 'reactflow';
import {Connection, Product} from "../../../data/DataTypes";
import "./ProductsPreviewEdge.scss";
import {NodeAmountField} from "../nodes/components/NodeAmountField";

export type ProductsPreviewEdgeType = {
    connections: Connection[],
    products: Product[],
}

export function ProductsPreviewEdge(props: EdgeProps) {
    const {sourceX, sourceY, targetX, targetY, sourcePosition, targetPosition, markerEnd, style} = props;
    const [edgePath, labelX, labelY] = getBezierPath({sourceX, sourceY, sourcePosition, targetX, targetY, targetPosition});
    const data: ProductsPreviewEdgeType = props.data;
    
    return (
        <>
            <BaseEdge path={edgePath} markerEnd={markerEnd} style={style} />
            <EdgeLabelRenderer>
                <div
                    style={{transform: `translate(-50%, -50%) translate(${labelX}px,${labelY}px)`,}}
                    className="products-preview-edge-label">
                    <div className="products-preview">
                        {data.connections.map(value => <div key={value.id}>
                            <div>{findById(value.productId, data.products).name}</div>
                            <NodeAmountField amount={value.amount}/>
                        </div>)}
                    </div>
                    <button title="Edit connections">
                        <span className="material-icons-round">settings_input_composite</span>
                    </button>
                </div>
            </EdgeLabelRenderer>
        </>
    );
}

function findById(id: string, list: Product[]) {
    return list.find(v => v.id === id);
}
