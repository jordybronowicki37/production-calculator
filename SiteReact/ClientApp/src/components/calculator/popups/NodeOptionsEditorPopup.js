import {Popup} from "../../popup/Popup";
import "./NodeOptionsEditorPopup.css";
import {Button, Spinner} from "reactstrap";
import {useRef, useState} from "react";
import {nodeCreateProduct, nodeCreateRecipe} from "../../../data/api/NodeAPI";

export function NodeOptionsEditorPopup({worksheetId, products, recipes, machines, hidden, onClose, options}) {
  let editor;
  
  switch (options.nodeType) {
    case "Spawn":
    case "End":
      editor = <NodeOptionsProductType worksheetId={worksheetId} options={options} products={products}/>;
      break;
    case "Production":
      editor = <NodeOptionsRecipeType worksheetId={worksheetId} options={options} products={products} recipes={recipes} machines={machines}/>;
      break;
    default:
      editor = <div className="node-options">ERROR</div>;
  }
  
  return (
    <Popup
      hidden={hidden}
      onClose={onClose}>
      {editor}
    </Popup>
  )
}

function NodeOptionsProductType({worksheetId, options, products}) {
  const {nodeType, position} = options;
  
  const containerRef = useRef();
  const [productFilter, setProductFilter] = useState("");
  const [selectedProduct, setSelectedProduct] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [errorNoProductSelected, setErrorNoProductSelected] = useState(false);
  
  const sortedProducts = [...products].sort((a,b) => (a.name > b.name) ? 1 : (b.name > a.name) ? -1 : 0);
  const filteredProducts = sortedProducts.filter(p => p.name.toLowerCase().includes(productFilter.toLowerCase()));
  
  return <div className={`node-options type-${nodeType.toLowerCase()}`} ref={containerRef}>
    <div className="header"><h2 className="m-0">New {nodeType}-node</h2></div>
    <div className="p-2">Please select a product</div>
    <div className="entity-selector">
      <input 
        type="text" 
        placeholder="Filter" 
        value={productFilter} 
        onChange={(e) => setProductFilter(e.target.value)}
        className="bg-dark border border-light rounded m-1 text-light"/>
      {
        filteredProducts.map(p => 
          <div 
            key={p.id} 
            className={`selector-item ${p.id === selectedProduct ? "selected" : ""}`} 
            onClick={() => {
              if (isLoading) return;
              setSelectedProduct(p.id);
              setErrorNoProductSelected(false);
            }}>
            {p.name}
          </div>)
      }
    </div>
    <div className="footer">
      <div hidden={!errorNoProductSelected} className="text-danger p-2">
        No product selected!
      </div>
      
      <Button color={errorNoProductSelected ? "danger":"light"} outline onClick={() => {
        if (isLoading) return;
        if (selectedProduct === "") {
          setErrorNoProductSelected(true);
          return;
        }
        setIsLoading(true);
        nodeCreateProduct(worksheetId, nodeType, position, selectedProduct).then(() => {
          const closeEvent = new CustomEvent("closePopup", {bubbles: true});
          containerRef.current.dispatchEvent(closeEvent);
        });
      }}>Create</Button>
      {isLoading && <Spinner color="light" size="sm"/>}
    </div>
  </div>;
}

function NodeOptionsRecipeType({worksheetId, products, recipes, machines, options}) {
  const {nodeType, position} = options;

  const containerRef = useRef();
  const [isLoading, setIsLoading] = useState(false);

  const [errorNoRecipeSelected, setErrorNoRecipeSelected] = useState(false);
  const [recipeFilter, setRecipeFilter] = useState("");
  const [selectedRecipe, setSelectedRecipe] = useState("");

  const [errorNoMachineSelected, setErrorNoMachineSelected] = useState(false);
  const [machineFilter, setMachineFilter] = useState("");
  const [selectedMachine, setSelectedMachine] = useState("");
  
  let recipesCopy = [...recipes].sort((a,b) => (a.name > b.name) ? 1 : (b.name > a.name) ? -1 : 0);
  recipesCopy = recipesCopy.filter(r => r.name.toLowerCase().includes(recipeFilter.toLowerCase()));
  if (selectedMachine !== "") recipesCopy = recipesCopy.filter(r => machines.find(m => m.id === selectedMachine).recipes.includes(r.id));
  
  let machinesCopy = [...machines].sort((a,b) => (a.name > b.name) ? 1 : (b.name > a.name) ? -1 : 0);
  machinesCopy = machinesCopy.filter(m => m.name.toLowerCase().includes(machineFilter.toLowerCase()));
  if (selectedRecipe !== "") machinesCopy = machinesCopy.filter(r => r.recipes.includes(recipes.find(m => m.id === selectedRecipe).id));

  return <div className={`node-options type-${nodeType.toLowerCase()}`} ref={containerRef}>
    <div className="header"><h2 className="m-0">New {nodeType}-node</h2></div>
    <div className="p-2">Please select a recipe and a machine</div>
    <div className="entity-selectors-container">
      <div>
        <div className="text-center">Recipes</div>
        <div className="entity-selector">
          <input
            type="text"
            placeholder="Filter"
            value={recipeFilter}
            onChange={(e) => setRecipeFilter(e.target.value)}
            className="bg-dark border border-light rounded m-1 text-light"/>
          {
            recipesCopy.map(r =>
              <div
                key={r.id}
                className={`selector-item ${r.id === selectedRecipe ? "selected" : ""}`}
                onClick={() => {
                  if (isLoading) return;
                  setErrorNoRecipeSelected(false);
                  if (r.id === selectedRecipe) setSelectedRecipe("");
                  else setSelectedRecipe(r.id);
                }}>
                {r.name}
              </div>)
          }
        </div>
      </div>
      <div>
        <div className="text-center">Machines</div>
        <div className="entity-selector">
          <input
            type="text"
            placeholder="Filter"
            value={machineFilter}
            onChange={(e) => setMachineFilter(e.target.value)}
            className="bg-dark border border-light rounded m-1 text-light"/>
          {
            machinesCopy.map(m =>
              <div
                key={m.id}
                className={`selector-item ${m.id === selectedMachine ? "selected" : ""}`}
                onClick={() => {
                  if (isLoading) return;
                  setErrorNoMachineSelected(false);
                  if (m.id === selectedMachine) setSelectedMachine("");
                  else setSelectedMachine(m.id);
                }}>
                {m.name}
              </div>)
          }
        </div>
      </div>
    </div>
    <div className="footer">
      <div hidden={!errorNoRecipeSelected} className="text-danger p-2">
        No recipe selected!
      </div>
      <div hidden={!errorNoMachineSelected} className="text-danger p-2">
        No machine selected!
      </div>

      <Button color={errorNoRecipeSelected || errorNoMachineSelected ? "danger":"light"} outline onClick={() => {
        if (isLoading) return;
        if (selectedRecipe === "") {
          setErrorNoRecipeSelected(true);
          return;
        }
        if (selectedMachine === "") {
          setErrorNoMachineSelected(true);
          return;
        }
        setIsLoading(true);
        nodeCreateRecipe(worksheetId, nodeType, position, selectedRecipe, selectedMachine).then(() => {
          const closeEvent = new CustomEvent("closePopup", {bubbles: true});
          containerRef.current.dispatchEvent(closeEvent);
        });
      }}>Create</Button>
      {isLoading && <Spinner color="light" size="sm"/>}
    </div>
  </div>;
}
