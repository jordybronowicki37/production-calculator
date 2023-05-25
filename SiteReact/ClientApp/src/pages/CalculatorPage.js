import "./CalculatorPage.css";
import {Calculator} from "../components/calculator/Calculator";
import {useDispatch, useSelector} from "react-redux";
import {useEffect} from "react";
import {fetchWorksheet} from "../data/api/WorksheetAPI";

export function CalculatorPage(props) {
  const worksheetId = props.match.params.id;
  const dispatch = useDispatch();
  const {worksheet, products, recipes, machines} = useSelector(state => state);
  console.log(worksheet)
  
  useEffect(() => {
    fetchWorksheet(worksheetId);
    
    return () => {
      dispatch({type:"unload_worksheet"});
    }
  }, []);
  
  return (
    <div>
      {worksheet != null && <Calculator worksheet={worksheet} machines={machines} products={products} recipes={recipes}/>}
    </div>
  );
}
