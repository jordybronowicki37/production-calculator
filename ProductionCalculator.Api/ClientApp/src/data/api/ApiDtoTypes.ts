import {
    Alert,
    Connection,
    Machine,
    NodePosition,
    NodeTypes,
    Product,
    ProductionTarget, ProductionTargetTypes,
    ThroughPut
} from "../DataTypes";

export type ConnectionDto = Connection;

export type EntityContainerDto = {
    id: string,
    name: string,
    recipes: RecipeDto[],
    products: ProductDto[],
    machines: MachineDto[],
}

export type MachineDto = Machine;

export type NodeDto = {
    id: string,
    type: NodeTypes,
    amount: number,
    position: NodePosition,
    machine: string | null,
    product: string | null,
    recipe: string | null,
    targets: ProductionTargetDto[],
}

export type ProductDto = Product;

export type ProjectDto = {
    id: string,
    name: string,
    entityContainer: EntityContainerDto,
    worksheets: WorksheetDto[]
}

export type ProductionTargetDto = ProductionTarget;

export type ProductionTargetCreateDto = {
    type: ProductionTargetTypes,
    amount: number,
}

export type RecipeDto = {
    id: string,
    name: string,
    inputThroughPuts: ThroughPutDto[]
    outputThroughPuts: ThroughPutDto[]
}

export type ThroughPutDto = ThroughPut;

export type WorksheetDto = {
    id: string,
    name: string,
    alerts: Alert[],
    connections: ConnectionDto[],
    nodes: NodeDto[]
}
