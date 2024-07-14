For easily creating production chains and calculating product throughput. With this project you never have to use your own calculator ever again for any factory building game.

# Node types
## Spawn-node
The spawn-node can create new products out of thin air. This would for example be a mine, some kind of collector or any other machine that indicated the starting point of the production process.

## Production-node
The production-node is the most important node type there is. This node represents a transformation of the input products. For this process a recipe has to be selected. A recipe describes the exact product amounts that are consumed or created by the node. In addition to specifying a recipe, this node type can also contain a certain machine. By specifying a machine type it is possible to work with upgrades/boosts. With this the node amount can be reduced by either decreasing the required input products, by increasing the output products or by making the overall process more efficient. This node type can represent a machine, a group of machines or a certain process. The amount of this node represents the scale of the process, how many machines or groups of machines are necessary.

## End-node
The end-node consumes the final resulting product or can consume byproducts that are leftover from the production process. This node can also be used to create an overflow mechanism or it can be used to set the production target for the entire system. This node can simulate the final product destination, the output of a factory, a drain or some kind of other removal system.

## Input-node
The input-node is similar to the spawn-node. The only change is that the node amount counts towards the inputs of a worksheet.

## Worksheet-node
With the worksheet-node you can use worksheets inside of other worksheets. With this node you can split up a complex production chain into smaller parts and manage them separately. The inputs and outputs of a worksheet are specified in the input-nodes and output-nodes of that worksheet. The spawn-nodes and end-nodes are not counted as they are internally handled in the worksheet. The amount field in this node represents the amount of worksheets (duplicate factories) that are required in the calculation. This node works very similar to the production-node, the only difference is that the recipe in this node is created inside of a worksheet and is therefore variable.

## Output-node
The output-node is similar to the end-node. The only change is that the node amount counts towards the outputs of a worksheet.

# Entity management
Every project can have different types of entities that can be used inside of it’s worksheets. You can choose one of the standard supported entity collections, create your own or find one in the marketplace.

# The calculation algorithm
For calculating the required amounts for all of the nodes, the production calculator uses an incremental stepping algorithm. During calculation an algorithm is executed several times until a stable solution if found. If no stable solution is found or the maximum iterations cap is reached, the worksheet is deemed to be unstable and unsolvable.

The calculation does not check which nodes are connected to who as this would be too complicated and also not be extendible enough. Instead all of the input and/or output connections are checked. If one of the connections displays a value that is higher than the current node amount, then it is indicating that a different node is requesting or providing more products. This would result in an increase of the node’s current amount. In addition all the other connections of this node are also increased in amount. During this redistribution of the new input/output product amounts, extra rules can be added. These rules are named targets. The logic of product amount redistribution and the available targets are described below.

## Targets
Targets can be applied onto all nodes and connections in a worksheet. These targets set certain rules for the calculation. All of the available rules are listed below.

### Node target types
* Exact amount
* Min amount
* Max amount

### Connection target types
* Percentage
* Priority
* Exact amount
* Min amount
* Max amount

## Machine efficiency
Recipe production can be boosted by using a more efficient machine. This mechanic allows the user to have lower and higher tier machines next to each other or compare the difference in the amount of machines required. For the production process the user can make the decision to use loads of cheap machines or use some expensive machines. A machine’s efficiency represents the initial boost in recipe execution. Any further upgrades for the machine come in the form of power-ups that are described below.

## Power-ups
Power-ups allow production-nodes to be more efficient in executing recipes. This is an additional boost besides the machine’s efficiency. A power-up can be used to indicate that a specific machine had received an upgrade.
