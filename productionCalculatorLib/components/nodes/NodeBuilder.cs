﻿using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.worksheet;

namespace productionCalculatorLib.components.nodes;

public class NodeBuilder<TNodeType> where TNodeType : ANode, new()
{
    private readonly Worksheet _worksheet;
    private Product? _product;
    private Recipe? _recipe;
    private float? _exactAmount = null;
    private float? _minAmount = null;
    private float? _maxAmount = null;

    public NodeBuilder(Worksheet worksheet)
    {
        _worksheet = worksheet;
    }

    public NodeBuilder<TNodeType> SetProduct(Product product)
    {
        if (!typeof(TNodeType).GetInterfaces().Contains(typeof(IHasProduct))) throw new InvalidOperationException("This node type does not support products");
        _product = product;
        return this;
    }

    public NodeBuilder<TNodeType> SetRecipe(Recipe recipe)
    {
        if (!typeof(TNodeType).GetInterfaces().Contains(typeof(IHasRecipe))) throw new InvalidOperationException("This node type does not support recipes");
        _recipe = recipe;
        return this;
    }

    public NodeBuilder<TNodeType> SetExactTarget(float amount)
    {
        if (_minAmount == null && _maxAmount == null) _exactAmount = amount;
        return this;
    }

    public NodeBuilder<TNodeType> SetMinMaxTarget(float? minAmount, float? maxAmount)
    {
        if (_exactAmount == null)
        {
            _minAmount = minAmount;
            _maxAmount = maxAmount;
        }
        return this;
    }

    public TNodeType Build()
    {
        TNodeType newNode = new();
        
        if (newNode is IHasProduct nodeProduct)
        {
            nodeProduct.Product = _product ?? throw new InvalidOperationException("No product set");
        }
        
        if (newNode is IHasRecipe nodeRecipe)
        {
            nodeRecipe.Recipe = _recipe ?? throw new InvalidOperationException("No recipe set");
        }

        if (_exactAmount != null)
        {
            newNode.SetExactTarget((float) _exactAmount);
        } 
        else if (_minAmount != null || _maxAmount != null)
        {
            newNode.SetMinMaxTarget(_minAmount, _maxAmount);
        }

        _worksheet.AddNode(newNode);
        return newNode;
    }
}