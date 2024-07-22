import "./AuthorizationPage.scss";
import * as React from "react";
import {CSSProperties, FormEvent, memo, useEffect, useState} from "react";
import {useLogin, useRegister} from "../data/api/AuthorizationAPI";
import {useHistory} from "react-router-dom";
import ReactFlow, {
  Background,
  Controls,
  DefaultEdgeOptions,
  Edge,
  Handle,
  MarkerType,
  MiniMap,
  Node,
  NodeChange,
  NodePositionChange, NodeProps, NodeTypes,
  Position,
  ReactFlowInstance,
  ReactFlowProvider
} from "reactflow";
import {NodeDragHandle} from "../components/calculator/nodes/components/NodeDragHandle";

const defaultEdgeOptions: DefaultEdgeOptions = {
  type: 'default',
  markerEnd: {type: MarkerType.Arrow},
  animated: true
};
const defaultNodeStyle: CSSProperties = {
  width: "min-content",
  padding: 0,
  textAlign: "initial",
  border: "none",
  borderRadius: "5px",
  backgroundColor: "transparent"
};
const defaultNodeOptions = {
  style: defaultNodeStyle,
  sourcePosition: Position.Right,
  targetPosition: Position.Left,
}
const NoEdgeConnectionsType: React.ComponentType<NodeProps> = memo(({data}) => {
  return <>{data.label}</>;
});
const nodeTypes: NodeTypes = {
  none: NoEdgeConnectionsType,
};

export function AuthorizationPage(): React.JSX.Element {
  const [isLoggingIn, setIsLoggingIn] = useState(true);

  return <div className="auth-page">
    <ReactFlowProvider>
      {
        isLoggingIn ?
          <RegisterCard switchCard={() => setIsLoggingIn(prev => !prev)}/> :
          <LoginCard switchCard={() => setIsLoggingIn(prev => !prev)}/>
      }
    </ReactFlowProvider>
  </div>
}

type AuthCardSharedTypes = {
  switchCard: () => void
}

function LoginCard({switchCard}: AuthCardSharedTypes): React.JSX.Element {
  const history = useHistory<History>();
  const {loginUser} = useLogin();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  function handleLoginUser(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    loginUser({email, password})
      .then(() => {
        history.push("/projects");
      })
      .catch(() => undefined);
  }

  const nodes: Node<{label: React.JSX.Element}, string>[] = [
    {
      id: "0",
      type: "none",
      data: {
        label: <TitleNode title="Login"/>
      },
      position: {x: 150, y: -110},
      ...defaultNodeOptions
    },
    {
      id: "1",
      type: "none",
      data: {
        label: <TextNode text="Hello welcome back! 👋"/>
      },
      position: {x: 150, y: -50},
      ...defaultNodeOptions
    },
    {
      id: "2",
      type: "input",
      data: {
        label: <TextInputNode name="email" displayName="Email" inputType="email" placeholder="Your email adress"
                              updateValue={setEmail}/>
      },
      position: {x: 0, y: 0},
      dragHandle: ".node-drag-handle",
      ...defaultNodeOptions
    },
    {
      id: "3",
      type: "input",
      data: {
        label: <TextInputNode name="password" displayName="Password" placeholder="Your password" inputType="password"
                              updateValue={setPassword}/>
      },
      position: {x: 0, y: 100},
      dragHandle: ".node-drag-handle",
      ...defaultNodeOptions
    },
    {
      id: "7",
      type: "none",
      data: {
        label: <LoginButtonNode/>
      },
      position: {x: 400, y: 75},
      ...defaultNodeOptions,
    },
    {
      id: "9",
      type: "none",
      data: {
        label: <SwitchScreenNode onSwitch={switchCard} text="Don't have an account yet?"/>
      },
      position: {x: 200, y: 200},
      ...defaultNodeOptions,
    },
  ];

  const edges: Edge[] = [
    {
      id: "2-7",
      source: "2",
      target: "7",
      targetHandle: "a",
    },
    {
      id: "3-7",
      source: "3",
      target: "7",
      targetHandle: "b",
    },
  ]

  return (
    <form onSubmit={handleLoginUser}>
      <AuthFlowChart initialNodes={nodes} initialEdges={edges}/>
    </form>
  )
}

function RegisterCard({switchCard}: AuthCardSharedTypes): React.JSX.Element {
  const history = useHistory<History>();
  const {registerUser} = useRegister();
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [password2, setPassword2] = useState("");

  function handleRegisterUser(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    registerUser({username, email, password, password2})
      .then(() => {
        history.push("/projects");
      })
      .catch(() => undefined);
  }

  const nodes: Node<{label: React.JSX.Element}, string>[] = [
    {
      id: "0",
      type: "none",
      data: {
        label: <TitleNode title="Register"/>
      },
      position: {x: 150, y: -110},
      ...defaultNodeOptions
    },
    {
      id: "1",
      type: "none",
      data: {
        label: <TextNode text="Hello welcome! 👋"/>
      },
      position: {x: 150, y: -50},
      ...defaultNodeOptions
    },
    {
      id: "2",
      type: "input",
      data: {
        label: <TextInputNode name="username" displayName="Username" inputType="text" placeholder="Your username"
                              updateValue={setUsername}/>
      },
      position: {x: 0, y: 0},
      dragHandle: ".node-drag-handle",
      ...defaultNodeOptions
    },
    {
      id: "3",
      type: "input",
      data: {
        label: <TextInputNode name="email" displayName="Email" inputType="email" placeholder="Your email-adress"
                              updateValue={setEmail}/>
      },
      position: {x: 0, y: 75},
      dragHandle: ".node-drag-handle",
      ...defaultNodeOptions
    },
    {
      id: "4",
      type: "input",
      data: {
        label: <TextInputNode name="password" displayName="Password" placeholder="Your password" inputType="password"
                              updateValue={setPassword}/>
      },
      position: {x: 0, y: 150},
      dragHandle: ".node-drag-handle",
      ...defaultNodeOptions
    },
    {
      id: "5",
      type: "input",
      data: {
        label: <TextInputNode name="password2" displayName="Repeat password" placeholder="Repeat password"
                              inputType="password"
                              updateValue={setPassword2}/>
      },
      position: {x: 0, y: 225},
      dragHandle: ".node-drag-handle",
      ...defaultNodeOptions
    },
    {
      id: "7",
      type: "input",
      data: {
        label: <RegisterButtonNode/>
      },
      position: {x: 300, y: 100},
      ...defaultNodeOptions,
    },
    {
      id: "8",
      type: "output",
      data: {
        label: <AccountNode/>
      },
      position: {x: 500, y: 100},
      ...defaultNodeOptions,
    },
    {
      id: "9",
      type: "none",
      data: {
        label: <SwitchScreenNode onSwitch={switchCard} text="Login instead"/>
      },
      position: {x: 300, y: 300},
      ...defaultNodeOptions,
    },
  ];

  const edges: Edge[] = [
    {
      id: "2-7",
      source: "2",
      target: "7",
      targetHandle: "a",
    },
    {
      id: "3-7",
      source: "3",
      target: "7",
      targetHandle: "b",
    },
    {
      id: "4-7",
      source: "4",
      target: "7",
      targetHandle: "c",
    },
    {
      id: "5-7",
      source: "5",
      target: "7",
      targetHandle: "d",
    },
    {
      id: "7-8",
      source: "7",
      target: "8",
    },
  ]

  return (
    <form onSubmit={handleRegisterUser}>
      <AuthFlowChart initialNodes={nodes} initialEdges={edges}/>
    </form>
  );
}

function AuthFlowChart({initialNodes, initialEdges}: { initialNodes: Node[], initialEdges: Edge[] }): React.JSX.Element {
  const [nodes, setNodes] = useState(initialNodes);
  const [reactFlowInstance, setReactFlowInstance] = useState<ReactFlowInstance | null>(null);

  useEffect(() => {
    if (reactFlowInstance != null) {
      reactFlowInstance.fitView();
    }
  }, [reactFlowInstance]);

  return (
    <ReactFlow
      className="flow-chart"
      nodes={nodes}
      edges={initialEdges}
      nodeTypes={nodeTypes}
      onNodesChange={(changes: NodeChange[]) => {
        for (const change of changes) {
          if (change.type !== "position") return;
          const positionChange = change as NodePositionChange;
          if (!positionChange.dragging || !positionChange.position) return;
          const node = nodes.find(v => v.id === positionChange.id);
          if (!node) return;
          node.position = positionChange.position;
          node.positionAbsolute = positionChange.positionAbsolute;
          setNodes([...nodes]);
        }
      }}
      onInit={setReactFlowInstance}
      defaultEdgeOptions={defaultEdgeOptions}>
      <MiniMap
        nodeStrokeColor="#777"
        nodeColor="transparent"
        maskColor="#3338"
        style={{backgroundColor: "#444"}}/>
      <Controls/>
      <Background color="#bbb" style={{backgroundColor: "#444"}}/>
    </ReactFlow>
  );
}

type TextInputNodeProps = {
  name: string,
  displayName: string,
  placeholder: string,
  inputType: string,
  updateValue: (value: string) => void,
}

function TextInputNode(props: TextInputNodeProps): React.JSX.Element {
  const {name, displayName, placeholder, inputType, updateValue} = props;
  const [currentValue, setCurrentValue] = useState("");

  return (
    <div className="text-input-node auth-page-custom-node">
      <header>
        <label htmlFor={name}>{displayName}</label>
        <NodeDragHandle/>
      </header>
      <input id={name} type={inputType} value={currentValue} placeholder={placeholder} onChange={e => {
        updateValue(e.target.value);
        setCurrentValue(e.target.value);
      }}/>
    </div>
  )
}

function TitleNode({title}: { title: string }): React.JSX.Element {
  return <h2 className="title-node auth-page-custom-node">{title}</h2>
}

function TextNode({text}: { text: string }): React.JSX.Element {
  return <p className="text-node auth-page-custom-node">{text}</p>
}

function SwitchScreenNode({text, onSwitch}: { text: string, onSwitch: () => void }): React.JSX.Element {
  return <button className="switch-node auth-page-custom-node" onClick={onSwitch} type="button">{text}</button>
}

function AccountNode(): React.JSX.Element {
  return (
    <div className="account-node auth-page-custom-node">
      <header>
        <i className='bx bx-user-circle'/>
        <h2>Account</h2>
      </header>
      <p>Your new account 🎉</p>
    </div>
  );
}

function LoginButtonNode(): React.JSX.Element {
  return (
    <>
      <Handle
        type="target"
        position={Position.Left}
        id="a"
        style={{top: 10, background: '#1a192b'}}
        isConnectable={true}
      />
      <Handle
        type="target"
        position={Position.Left}
        id="b"
        style={{bottom: 5, top: 'auto', background: '#1a192b'}}
        isConnectable={true}
      />
      <div className="submit-node auth-page-custom-node">
        <button type="submit">Login</button>
      </div>
    </>
  )
}

function RegisterButtonNode(): React.JSX.Element {
  return (
    <>
      <Handle
        type="target"
        position={Position.Left}
        id="a"
        style={{top: 7, background: '#1a192b'}}
        isConnectable={true}
      />
      <Handle
        type="target"
        position={Position.Left}
        id="b"
        style={{top: 15, background: '#1a192b'}}
        isConnectable={true}
      />
      <Handle
        type="target"
        position={Position.Left}
        id="c"
        style={{bottom: 9, top: 'auto', background: '#1a192b'}}
        isConnectable={true}
      />
      <Handle
        type="target"
        position={Position.Left}
        id="d"
        style={{bottom: 1, top: 'auto', background: '#1a192b'}}
        isConnectable={true}
      />
      <div className="submit-node auth-page-custom-node">
        <button type="submit">Register</button>
      </div>
    </>
  )
}
