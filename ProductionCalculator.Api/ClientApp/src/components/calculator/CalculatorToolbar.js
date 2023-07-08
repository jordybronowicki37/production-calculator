import "./CalculatorToolbar.css";
import {Link} from "react-router-dom";
import {useState} from "react";

export function CalculatorToolbar({calculator}) {
  const [openMenu, setOpenMenu] = useState("none");
  
  return (
    <div className="toolbar">
      <div>
        <button onClick={() => setOpenMenu(openMenu==="worksheet"?"none":"worksheet")}
                type="button" className={openMenu==="worksheet"?"selected":""}>Worksheet</button>
        <div className="drop-menu" hidden={openMenu!=="worksheet"}>
          <Link to="/worksheets"><button type="button">View all</button></Link>
          <button type="button">Change name</button>
        </div>
      </div>
      
      <div>
        <button onClick={() => setOpenMenu(openMenu==="products"?"none":"products")}
                type="button" className={openMenu==="products"?"selected":""}>Products</button>
        <div className="drop-menu" hidden={openMenu!=="products"}>
          <button type="button" onClick={() => calculator.setState({popupProductManagerOpen:true})}>View products</button>
          <button type="button">Add product</button>
        </div>
      </div>
      
      <div>
        <button onClick={() => setOpenMenu(openMenu==="recipes"?"none":"recipes")}
                type="button" className={openMenu==="recipes"?"selected":""}>Recipes</button>
        <div className="drop-menu" hidden={openMenu!=="recipes"}>
          <button type="button" onClick={() => calculator.setState({popupRecipeManagerOpen:true})}>View recipes</button>
          <button type="button" onClick={() => calculator.setState({popupRecipeCreatorOpen:true})}>Add recipe</button>
        </div>
      </div>
      
      <div>
        <button onClick={() => setOpenMenu(openMenu==="nodes"?"none":"nodes")}
                type="button" className={openMenu==="nodes"?"selected":""}>Nodes</button>
        <div className="drop-menu" hidden={openMenu!=="nodes"}>
          <div className="item-with-info">
            <div className="draggable-node-icon flex-grow-1" draggable
                 onClick={() => calculator.onAddNode("Spawn")}
                 onDragStart={(event) => calculator.onDragStart(event, "Spawn")}>Spawn</div>
            <i className='bx bx-info-circle icon' onClick={() => calculator.setState({popupNodeSpawnPreviewOpen:true})}></i>
          </div>
          <div className="item-with-info">
            <div className="draggable-node-icon flex-grow-1" draggable
                 onClick={() => calculator.onAddNode("Production")}
                 onDragStart={(event) => calculator.onDragStart(event, "Production")}>Production</div>
            <i className='bx bx-info-circle icon' onClick={() => calculator.setState({popupNodeProductionPreviewOpen:true})}></i>
          </div>
          <div className="item-with-info">
            <div className="draggable-node-icon flex-grow-1" draggable
                 onClick={() => calculator.onAddNode("End")}
                 onDragStart={(event) => calculator.onDragStart(event, "End")}>End</div>
            <i className='bx bx-info-circle icon' onClick={() => calculator.setState({popupNodeEndPreviewOpen:true})}></i>
          </div>
        </div>
      </div>
    </div>
  );
}