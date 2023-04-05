import "./CalculatorPage.css";
import {Calculator} from "../components/calculator/Calculator";
import {useDispatch, useSelector} from "react-redux";
import {useEffect} from "react";

export function CalculatorPage(props) {
  const worksheetId = props.match.params.id;
  const worksheet = useSelector(state => state.worksheet);
  const dispatch = useDispatch();
  
  useEffect(() => {
    return () => {
      dispatch({type:"unload_worksheet"});
    }
  }, []);
  
  return (
    <div>
      <div className="worksheet-page-top">
        <h1>Calculator</h1>
        <div className="worksheet-info">
          <div>Worksheet name: </div>
          <div>{worksheet?worksheet.name:""}</div>
          <div>EC name: </div>
          <div>?</div>
          <div>Author: </div>
          <div>You</div>
        </div>
      </div>
      
      <Calculator worksheetId={worksheetId}/>
    </div>
  );
}
