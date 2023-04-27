import "./CalculatorPage.css";
import {Calculator} from "../components/calculator/Calculator";
import {useDispatch, useSelector} from "react-redux";
import {useEffect} from "react";

export function CalculatorPage(props) {
  const worksheetId = props.match.params.id;
  const dispatch = useDispatch();
  
  useEffect(() => {
    return () => {
      dispatch({type:"unload_worksheet"});
    }
  }, []);
  
  return (
    <div>
      <Calculator worksheetId={worksheetId}/>
    </div>
  );
}
