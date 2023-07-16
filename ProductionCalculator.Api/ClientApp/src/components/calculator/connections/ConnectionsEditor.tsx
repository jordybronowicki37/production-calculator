import {Connection, Product} from "../../../data/DataTypes";
import {RoundedAmountField} from "../../misc/RoundedAmountField";
import "./ConnectionsEditor.scss";
import {connectionEdit} from "../../../data/api/ConnectionAPI";

export type ConnectionsEditorType = {
    worksheetId: string,
    connections: Connection[],
    products: Product[],
    edgeId: string,
}

export function ConnectionsEditor({worksheetId, connections, edgeId, products}: ConnectionsEditorType) {
    const nodeInId = edgeId.split(";")[0];
    const nodeOutId = edgeId.split(";")[1];
    const relevantConnections = connections.filter(c => c.inputNodeId === nodeInId && c.outputNodeId === nodeOutId);
    const sortedProducts = [...products].sort((a,b) => (a.name > b.name) ? 1 : ((b.name > a.name) ? -1 : 0))

    return (
        <div className="connections-editor">
            <div className="connections-editor-header">
                <h3>Connections</h3>
            </div>
            <div className="connections-list">
                {relevantConnections.map(c => <div key={c.id} className="connection-item">
                    <select 
                        name="Connection product" 
                        value={findById(c.productId, products).id}
                        onChange={e => {
                            connectionEdit(worksheetId, c.id, e.target.value);
                        }}
                    >
                        {sortedProducts.map(p => <option key={p.id} value={p.id}>{p.name}</option>)}
                    </select>
                    <RoundedAmountField amount={c.amount}/>
                </div>)}
            </div>
        </div>
    );
}

function findById(id: string, list: Product[]) {
    return list.find(v => v.id === id);
}
