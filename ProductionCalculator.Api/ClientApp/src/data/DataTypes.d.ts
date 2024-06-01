export type Alert = {
    id: string,
    message: string,
    level: AlertLevel,
    nodeId?: string,
    connectionId?: string,
    productId?: string,
}

export enum AlertLevel {
    Unknown = "Unknown",
    Info = "Info",
    Warning = "Warning",
    Error = "Error",
} 

export type Connection = {
    id: string,
    amount: number,
    productId: string,
    inputNodeId: string,
    outputNodeId: string,
} 

export type EntityContainer = {
    id: string,
    name: string,
    recipes: Recipe[],
    products: Product[],
    machines: Machine[],
}

export type Machine = {
    id: string,
    name: string,
    recipes: string[],
}

export type Node = {
    id: string,
    type: NodeTypes,
    amount: number,
    position: NodePosition,
    machine: string | null,
    product: string | null,
    recipe: string | null,
    targets: ProductionTarget[],
}

export type NodeTypes = "End" | "Input" | "Production" | "Spawn" | "Output" | "Worksheet";

export type NodePosition = {
    x: number,
    y: number,
}

export type Product = {
    id: string,
    name: string,
}

export type Project = {
    id: string,
    name: string,
    entityContainerId: string,
}

export type ProductionTarget = {
    type: ProductionTargetTypes,
    amount: number,
}

export type ProductionTargetTypes = "ExactAmount" | "MinAmount" | "MaxAmount";

export type Recipe = {
    id: string,
    name: string,
    inputThroughPuts: ThroughPut[]
    outputThroughPuts: ThroughPut[]
}

export type ThroughPut = {
    product: string,
    amount: number,
}

export type Worksheet = {
    id: string,
    name: string,
    alerts: Alert[],
    connections: Connection[],
    nodes: Node[]
}