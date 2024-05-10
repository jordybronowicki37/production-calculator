using System.Collections.Generic;
using System.Linq;
using productionCalculatorLib.components.calculator;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.worksheet;
using Xunit;
using Xunit.Abstractions;

namespace ProductionCalculator.Core.Test.calculator;

public class LimitCalculatorTest
{
    private readonly ITestOutputHelper _logger;
    
    public LimitCalculatorTest(ITestOutputHelper logger)
    {
        _logger = logger;
    }

    [Theory]
    [MemberData(nameof(SetupData))]
    public void ReCalculateTest(string testName, SetupParams setup, float[] answers)
    {
        _logger.WriteLine("Executing setup test: {0}", testName);
        
        // Arrange
        var nodes = setup.Worksheet.Nodes;
        
        // Act
        new CalculatorLimit(setup.Worksheet, setup.EntityContainer).ReCalculateAmounts();
        
        // Assert
        for (var i = 0; i < answers.Length; i++)
        {
            Assert.True(CalculatorLimit.CompareFloatingPointNumbers(answers[i], nodes.ElementAt(i).Amount));
        }
        Assert.True(setup.Worksheet.CalculationSucceeded);
        Assert.Empty(setup.Worksheet.CalculationError);
    }

    #region TestData
    public static IEnumerable<object[]> SetupData()
    {
        return new List<object[]>
        {
            new object[] {"SimpleSetup 1", SetTarget(SimpleSetup(), 0, 60f), new float[] {60, 2, 20}},
            new object[] {"SimpleSetup 2", SetTarget(SimpleSetup(), 1, 2f), new float[] {60, 2, 20}},
            new object[] {"SimpleSetup 3", SetTarget(SimpleSetup(), 2, 20f), new float[] {60, 2, 20}},
            new object[] {"DoubleSpawnSetup 1", SetTarget(DoubleSpawnSetup(), 0, 60), new float[] {60, 10, 2, 20}},
            new object[] {"DoubleSpawnSetup 2", SetTarget(DoubleSpawnSetup(), 1, 10), new float[] {60, 10, 2, 20}},
            new object[] {"DoubleSpawnSetup 3", SetTarget(DoubleSpawnSetup(), 2, 2), new float[] {60, 10, 2, 20}},
            new object[] {"DoubleSpawnSetup 4", SetTarget(DoubleSpawnSetup(), 3, 20), new float[] {60, 10, 2, 20}},
            new object[] {"DoubleEndSetup 1", SetTarget(DoubleEndSetup(), 0, 60), new float[] {60, 2, 40, 20}},
            new object[] {"DoubleEndSetup 2", SetTarget(DoubleEndSetup(), 1, 2), new float[] {60, 2, 40, 20}},
            new object[] {"DoubleEndSetup 3", SetTarget(DoubleEndSetup(), 2, 40), new float[] {60, 2, 40, 20}},
            new object[] {"DoubleEndSetup 4", SetTarget(DoubleEndSetup(), 3, 20), new float[] {60, 2, 40, 20}},
            new object[] {"DoubleProductionSetup 1", SetTarget(DoubleProductionSetup(), 0, 90), new float[] {90, 3, 2, 20}},
            new object[] {"DoubleProductionSetup 2", SetTarget(DoubleProductionSetup(), 1, 3), new float[] {90, 3, 2, 20}},
            new object[] {"DoubleProductionSetup 3", SetTarget(DoubleProductionSetup(), 2, 2), new float[] {90, 3, 2, 20}},
            new object[] {"DoubleProductionSetup 4", SetTarget(DoubleProductionSetup(), 3, 20), new float[] {90, 3, 2, 20}},
            new object[] {"SimpleOverflowSetup 1", SetTarget(SetTarget(SimpleOverflowSetup(), 1, 3), 2, 20), new float[] {90, 3, 20, 10}},
            new object[] {"SimpleOverflowSetup 2", SetTarget(SetTarget(SimpleOverflowSetup(), 1, 3), 3, 10), new float[] {90, 3, 20, 10}},
            new object[] {"SplitAndMergeSetup 1", SetTarget(SplitAndMergeSetup(), 0, 30), new float[] {30, 1, 1, 2, 4, 4}},
        };
    }
    #endregion TestData
    
    public record SetupParams(Worksheet Worksheet, EntityContainer EntityContainer);

    private static SetupParams SetTarget(SetupParams setup, int nodeNr, float target)
    {
        setup.Worksheet.Nodes.ElementAt(nodeNr).SetExactTarget(target);
        return setup;
    }

    #region Setups
    private static SetupParams SimpleSetup()
    {
        var entityContainer = new EntityContainer();
        var worksheet = new Worksheet("Iron ingot smelting", entityContainer.Id);

        var productIronOre = entityContainer.GetOrGenerateProduct("Iron ore");
        var productIronIngot = entityContainer.GetOrGenerateProduct("Iron ingot");
        
        var machineSmelter = entityContainer.GenerateMachine("Smelter");

        var recipeIronIngot = entityContainer.GetRecipeBuilder("Iron ingot", machineSmelter)
            .AddInput(productIronOre, 30)
            .AddOutput(productIronIngot, 10).Build();

        var node1 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productIronOre).Build();
        var node2 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronIngot, machineSmelter).Build();
        var node3 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productIronIngot).Build();
        
        worksheet.GetConnectionBuilder(node1, node2, productIronOre).Build();
        worksheet.GetConnectionBuilder(node2, node3, productIronIngot).Build();

        return new SetupParams(worksheet, entityContainer);
    }

    private static SetupParams DoubleSpawnSetup()
    {
        var entityContainer = new EntityContainer();
        var worksheet = new Worksheet("Steel ingot smelting", entityContainer.Id);

        var productIronOre = entityContainer.GetOrGenerateProduct("Iron ore");
        var productCoal = entityContainer.GetOrGenerateProduct("Coal");
        var productSteelIngot = entityContainer.GetOrGenerateProduct("Steel ingot");
        
        var machineSmelter = entityContainer.GenerateMachine("Smelter");

        var recipeIronIngot = entityContainer.GetRecipeBuilder("Steel ingot", machineSmelter)
            .AddInput(productIronOre, 30)
            .AddInput(productCoal, 5)
            .AddOutput(productSteelIngot, 10).Build();
        
        var node1 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productIronOre).Build();
        var node2 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productCoal).Build();
        var node3 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronIngot, machineSmelter).Build();
        var node4 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productSteelIngot).Build();
        
        worksheet.GetConnectionBuilder(node1, node3, productIronOre).Build();
        worksheet.GetConnectionBuilder(node2, node3, productCoal).Build();
        worksheet.GetConnectionBuilder(node3, node4, productSteelIngot).Build();

        return new SetupParams(worksheet, entityContainer);
    }
    
    private static SetupParams DoubleEndSetup()
    {
        var entityContainer = new EntityContainer();
        var worksheet = new Worksheet("Oxygen electrolysis", entityContainer.Id);

        var productWater = entityContainer.GetOrGenerateProduct("Water");
        var productHydrogen = entityContainer.GetOrGenerateProduct("Hydrogen");
        var productOxygen = entityContainer.GetOrGenerateProduct("Oxygen");
        
        var machineElectrolyzer = entityContainer.GenerateMachine("Electrolyzer");
        
        var recipeHydrogen = entityContainer.GetRecipeBuilder("Hydrogen", machineElectrolyzer)
            .AddInput(productWater, 30)
            .AddOutput(productHydrogen, 20)
            .AddOutput(productOxygen, 10).Build();
        
        var node1 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productWater).Build();
        var node2 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeHydrogen, machineElectrolyzer).Build();
        var node3 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productHydrogen).Build();
        var node4 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productOxygen).Build();
        
        worksheet.GetConnectionBuilder(node1, node2, productWater).Build();
        worksheet.GetConnectionBuilder(node2, node3, productHydrogen).Build();
        worksheet.GetConnectionBuilder(node2, node4, productOxygen).Build();

        return new SetupParams(worksheet, entityContainer);
    }
    
    private static SetupParams DoubleProductionSetup()
    {
        var entityContainer = new EntityContainer();
        var worksheet = new Worksheet("Iron rod construction", entityContainer.Id);

        var productIronOre = entityContainer.GetOrGenerateProduct("Iron ore");
        var productIronIngot = entityContainer.GetOrGenerateProduct("Iron ingot");
        var productIronRod = entityContainer.GetOrGenerateProduct("Iron rod");
        
        var machineSmelter = entityContainer.GenerateMachine("Smelter");
        var machineConstructor = entityContainer.GenerateMachine("Constructor");

        var recipeIronIngot = entityContainer.GetRecipeBuilder("Iron ingot", machineSmelter)
            .AddInput(productIronOre, 30)
            .AddOutput(productIronIngot, 10).Build();

        var recipeIronRod = entityContainer.GetRecipeBuilder("Iron rod", machineConstructor)
            .AddInput(productIronIngot, 15)
            .AddOutput(productIronRod, 10).Build();

        var node1 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productIronOre).Build();
        var node2 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronIngot, machineSmelter).Build();
        var node3 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronRod, machineConstructor).Build();
        var node4 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productIronRod).Build();
        
        worksheet.GetConnectionBuilder(node1, node2, productIronOre).Build();
        worksheet.GetConnectionBuilder(node2, node3, productIronIngot).Build();
        worksheet.GetConnectionBuilder(node3, node4, productIronRod).Build();

        return new SetupParams(worksheet, entityContainer);
    }
    
    private static SetupParams SimpleOverflowSetup()
    {
        var entityContainer = new EntityContainer();
        var worksheet = new Worksheet("Iron ingot smelting", entityContainer.Id);

        var productIronOre = entityContainer.GetOrGenerateProduct("Iron ore");
        var productIronIngot = entityContainer.GetOrGenerateProduct("Iron ingot");
        
        var machineSmelter = entityContainer.GenerateMachine("Smelter");

        var recipeIronIngot = entityContainer.GetRecipeBuilder("Iron ingot", machineSmelter)
            .AddInput(productIronOre, 30)
            .AddOutput(productIronIngot, 10).Build();

        var node1 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productIronOre).Build();
        var node2 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronIngot, machineSmelter).Build();
        var node3 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productIronIngot).Build();
        var node4 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productIronIngot).Build();
        
        worksheet.GetConnectionBuilder(node1, node2, productIronOre).Build();
        worksheet.GetConnectionBuilder(node2, node3, productIronIngot).Build();
        worksheet.GetConnectionBuilder(node2, node4, productIronIngot).Build();

        return new SetupParams(worksheet, entityContainer);
    }
    
    private static SetupParams SplitAndMergeSetup()
    {
        var entityContainer = new EntityContainer();
        var worksheet = new Worksheet("Iron ingot smelting", entityContainer.Id);

        var productIronOre = entityContainer.GetOrGenerateProduct("Iron ore");
        var productIronIngot = entityContainer.GetOrGenerateProduct("Iron ingot");
        var productIronBar = entityContainer.GetOrGenerateProduct("Iron bar");
        var productIronSheet = entityContainer.GetOrGenerateProduct("Iron sheet");
        var productIronChair = entityContainer.GetOrGenerateProduct("Iron chair");
        
        var machineSmelter = entityContainer.GenerateMachine("Smelter");
        var machineConstructor = entityContainer.GenerateMachine("Constructor");
        var machineAssembler = entityContainer.GenerateMachine("Assembler");

        var recipeIronIngot = entityContainer.GetRecipeBuilder("Iron ingot", machineSmelter)
            .AddInput(productIronOre, 30)
            .AddOutput(productIronIngot, 10).Build();
        var recipeIronBar = entityContainer.GetRecipeBuilder("Iron bar", machineConstructor)
            .AddInput(productIronIngot, 5)
            .AddOutput(productIronBar, 10).Build();
        var recipeIronSheet = entityContainer.GetRecipeBuilder("Iron sheet", machineConstructor)
            .AddInput(productIronIngot, 2.5f)
            .AddOutput(productIronSheet, 2.5f).Build();
        var recipeIronChair = entityContainer.GetRecipeBuilder("Iron chair", machineAssembler)
            .AddInput(productIronBar, 2.5f)
            .AddInput(productIronSheet, 1.25f)
            .AddOutput(productIronChair, 1).Build();

        var node1 = worksheet.GetNodeBuilder<SpawnNode>().SetProduct(productIronOre).Build();
        var node2 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronIngot, machineSmelter).Build();
        var node3 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronBar, machineConstructor).Build();
        var node4 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronSheet, machineConstructor).Build();
        var node5 = worksheet.GetNodeBuilder<ProductionNode>().SetRecipe(recipeIronChair, machineAssembler).Build();
        var node6 = worksheet.GetNodeBuilder<EndNode>().SetProduct(productIronChair).Build();
        
        worksheet.GetConnectionBuilder(node1, node2, productIronOre).Build();
        worksheet.GetConnectionBuilder(node2, node3, productIronIngot).Build();
        worksheet.GetConnectionBuilder(node2, node4, productIronIngot).Build();
        worksheet.GetConnectionBuilder(node3, node5, productIronBar).Build();
        worksheet.GetConnectionBuilder(node4, node5, productIronSheet).Build();
        worksheet.GetConnectionBuilder(node5, node6, productIronChair).Build();

        return new SetupParams(worksheet, entityContainer);
    }
    #endregion Setups
}
