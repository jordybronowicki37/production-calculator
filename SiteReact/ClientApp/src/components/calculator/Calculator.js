import {Component} from "react";
import {NodeProduction} from "../nodes/NodeProduction";
import {NodeSpawn} from "../nodes/NodeSpawn";
import {NodeEnd} from "../nodes/NodeEnd";
import ReactFlow, {MiniMap, Controls, applyEdgeChanges, applyNodeChanges, addEdge, Background, MarkerType} from 'react-flow-renderer';
import "./Calculator.css"

export class Calculator extends Component {
  defaultEdgeOptions = {type: 'smoothstep', markerEnd: {type: MarkerType.Arrow}}
  
  constructor(props) {
    super(props);
    this.state = {
      nodes: [],
      edges: [],
    }
    this.fetchWorksheet()
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
                   onConnect={this.onConnect}
                   defaultEdgeOptions={this.defaultEdgeOptions}>
          <MiniMap/>
          <Controls/>
          <Background/>
        </ReactFlow>
        <div className="testNodes">
          <NodeProduction/>
          <NodeSpawn/>
          <NodeEnd/>
        </div>
      </div>
    );
  }

  setNodes = nodes => this.setState({nodes: nodes});
  setEdges = edges => this.setState({edges:edges});
  
  onNodesChange = (changes) => this.setNodes(applyNodeChanges(changes, this.state.nodes));
  onEdgesChange = (changes) => this.setEdges(applyEdgeChanges(changes, this.state.edges));
  onConnect = edge => this.setEdges(addEdge(edge, this.state.edges));
  
  fetchWorksheet() {
    fetch("https://localhost:7291/worksheet/0").then(response => {
      response.json().then(worksheet => {
        console.log(worksheet);
        
        let nodes = worksheet.nodes.map((node, index) => {
          let innerContent = ({
            "Spawn":<NodeSpawn></NodeSpawn>,
            "Production":<NodeProduction/>,
            "End":<NodeEnd/>
          })[node.type];
          
          let nodeType = ({
            "Spawn":"input",
            "Production":"default",
            "End":"output"
          })[node.type]

          return {
            id: node.id.toString(),
            type: nodeType,
            style: {width:"min-content", padding:0},
            data: { label: innerContent },
            position: { x: 300, y: index*150 },
          };
        });
        
        let edges = worksheet.connections.map(edge => {
          let id1 = edge.inputNodeId.toString();
          let id2 = edge.outputNodeId.toString();
          return {
            id: `${id1}-${id2}`, 
            source: id1, 
            target: id2
          }
        })
        
        this.setState({
          nodes: nodes,
          edges: edges
        })
      })
    })
  }
}