export type Connection = {
    id: string,
    amount: number,
    productId: string,
    inputNodeId: string,
    outputNodeId: string,
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
    entityContainerId: string,
    name: string,
}

export type Recipe = {
    id: string,
    name: string,
    inputThroughPuts: ThroughPut[]
    outputThroughPuts: ThroughPut[]
}

export type ProductionTarget = {
    type: ProductionTargetTypes,
    amount: number,
}

export type ProductionTargetTypes = "ExactAmount" | "MinAmount" | "MaxAmount";

export type ThroughPut = {
    product: string,
    amount: number,
}

export type Worksheet = {
    id: string,
    name: string,
    calculationError: string,
    calculationSucceeded: boolean,
    connections: Connection[],
    nodes: Node[]
}