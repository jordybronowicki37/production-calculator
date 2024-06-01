import './Node.scss';
import './NodeProduction.scss';
import {TargetManager} from "../targets/TargetManager";
import {ReactNode, useState} from "react";
import {ActiveTargetsIcon} from "../targets/ActiveTargetsIcon";
import {NodeDragHandle} from "./components/NodeDragHandle";
import {PowerUpIcon} from "../powerUps/PowerUpIcon";
import {RoundedAmountField} from "../../misc/RoundedAmountField";
import {Machine, Node, Product, Recipe, ThroughPut, Worksheet} from "../../../data/DataTypes";
import {useSelector} from "react-redux";
import {StoreStates} from "../../../data/DataStore";
import {WarningAlertIcon} from "../alerts/WarningAlertIcon";
import {ErrorAlertIcon} from "../alerts/ErrorAlertIcon";

export type NodeProductionProps = {
  worksheetId: string, 
  node: Node, 
  machine: Machine, 
  recipe: Recipe, 
  products: Product[], 
  previewMode: boolean
}

export function NodeProduction({worksheetId, node, machine, recipe, products, previewMode}: NodeProductionProps) {
  const [editorOpen, setEditorOpen] = useState(false);
  const worksheets = useSelector<StoreStates, Worksheet[]>(state => state.worksheets);
  const alerts = worksheets.find(w => w.id === worksheetId)!.alerts.filter(a => a.nodeId === node.id);

  let machineField: ReactNode, recipeField: ReactNode, amountField: ReactNode, productInList: ReactNode, productOutList: ReactNode, targetEditor: ReactNode;

  if (previewMode) {
    machineField = <div className="preview-field">machine</div>;
    recipeField = <div className="preview-field">recipe</div>;
    amountField = <div className="preview-field">0</div>;
    productInList = 
      <div className="node-list">
        <div>Inputs</div>
        <div>
          <div className="previewField">name</div>
          <div className="previewField">0</div>
        </div>
      </div>;
    productOutList = 
      <div className="node-list">
        <div>Outputs</div>
        <div>
          <div className="previewField">name</div>
          <div className="previewField">0</div>
        </div>
      </div>;
    targetEditor = <div></div>;
  } 
  else 
  {
    const {id, targets, amount} = node;

    machineField = <div>{machine.name}</div>;
    recipeField = <div>{recipe.name}</div>;
    amountField = <RoundedAmountField amount={amount}/>;
    productInList = generateProductList("Inputs", recipe.inputThroughPuts, products, amount);
    productOutList = generateProductList("Outputs", recipe.outputThroughPuts, products, amount);
    
    targetEditor =
      <div className="target-editor-wrapper" hidden={!editorOpen}>
        <button type="button" className="popup-close-button" onClick={() => setEditorOpen(false)}>
          <i className='bx bx-x'></i>
        </button>
        <TargetManager worksheetId={worksheetId} nodeId={id} targets={targets}></TargetManager>
      </div>
  }

  return (
    <div className="node node-production">
      <div className="top-container">
        <h3>Production</h3>
        <NodeDragHandle/>
        <PowerUpIcon powerUps={[]} onOpenEditor={() => {}}/>
        <ActiveTargetsIcon targets={node?node.targets:[]} onOpenEditor={() => setEditorOpen(true)}/>
        <WarningAlertIcon alerts={alerts} onOpenEditor={() => {}} />
        <ErrorAlertIcon alerts={alerts} onOpenEditor={() => {}} />
      </div>
      <div className="content-container">
        <div className="content-table">
          <div>Machine:</div>
          {machineField}
          <div>Recipe:</div>
          {recipeField}
          <div>Amount:</div>
          {amountField}
          {productInList}
          {productOutList}
        </div>
      </div>
      {targetEditor}
    </div>
  );
}

function generateProductList(title: string, throughputList: ThroughPut[], productList: Product[], productionAmount: number) {
  return (
    <div className="node-list">
      <div>{title}</div>
      {throughputList.map((v, i) => {
        const product = findProduct(productList, v.product);
        return (
          <div key={i}>
            <div className="recipe-product">{product.name}:</div>
            <RoundedAmountField amount={v.amount*productionAmount}/>
          </div>
        )
      })}
    </div>
  );
}

function findProduct(products: Product[], id: string): Product {
  return products.find(v => v.id === id);
}
