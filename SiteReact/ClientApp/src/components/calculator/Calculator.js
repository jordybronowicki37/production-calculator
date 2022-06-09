import {Component} from "react";
import {NodeProduction} from "../nodes/NodeProduction";
import {NodeSpawn} from "../nodes/NodeSpawn";
import {NodeEnd} from "../nodes/NodeEnd";
import ReactFlow, {MiniMap, Controls, applyEdgeChanges, applyNodeChanges} from 'react-flow-renderer';
import "./Calculator.css"

export class Calculator extends Component {
  constructor(props) {
    super(props);
    this.state = {
      nodes: [
        {
          id: '1',
          type: 'input',
          data: { label: <NodeSpawn></NodeSpawn> },
          position: { x: 250, y: 25 },
          draggable: true
        },
        {
          id: '2',
          style: {width:"min-content", padding:0},
          data: { label: <NodeProduction></NodeProduction> },
          position: { x: 100, y: 125 },
        },
        {
          id: '3',
          type: 'output',
          data: { label: <NodeEnd></NodeEnd> },
          position: { x: 250, y: 250 },
        },
      ],
      edges: [
        { id: 'e1-2', source: '1', target: '2' },
        { id: 'e2-3', source: '2', target: '3', animated: true },
      ],
    }
  }

  render() {
    return (
      <div>
        <h1>Calculator</h1>
        <ReactFlow className="flowChart"
                   nodes={this.state.nodes}
                   edges={this.state.edges}
                   onNodesChange={this.onNodesChange}
                   onEdgesChange={this.onEdgesChange}
                   onConnect={this.onConnect}>
          <MiniMap />
          <Controls />
        </ReactFlow>
        {/*<NodeProduction></NodeProduction>*/}
        {/*<NodeSpawn></NodeSpawn>*/}
        {/*<NodeEnd></NodeEnd>*/}
      </div>
    );
  }

  setNodes = nodes => this.setState({nodes: nodes});
  setEdges = edges => this.setState({edges:edges});
  onNodesChange = (changes) => this.setNodes(applyNodeChanges(changes, this.state.nodes));
  onEdgesChange = (changes) => this.setEdges(applyEdgeChanges(changes, this.state.nodes));
  onConnect = changes => console.log(changes);
}