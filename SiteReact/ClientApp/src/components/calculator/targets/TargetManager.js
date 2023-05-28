import "./TargetManager.css"
import {useEffect, useState} from "react";
import {setTargets} from "../../../data/api/TargetAPI";

export function TargetManager({worksheetId, nodeId, targets}) {
  const [mode, setMode] = useState("none");
  const [exactAmount, setExactAmount] = useState(getInitialTargetValue(targets, "ExactAmount"));
  const [minAmount, setMinAmount] = useState(getInitialTargetValue(targets, "MinAmount"));
  const [maxAmount, setMaxAmount] = useState(getInitialTargetValue(targets, "MaxAmount"));
  
  useEffect(() => {
    let newTargets = [];

    if (exactAmount > 0) newTargets.push({type:"ExactAmount", amount:exactAmount});
    if (minAmount > 0) newTargets.push({type:"MinAmount", amount:minAmount});
    if (maxAmount > 0) newTargets.push({type:"MaxAmount", amount:maxAmount});
    
    if (checkTargetListCopy(newTargets, targets)) return;

    setTargets(worksheetId, nodeId, newTargets);
  }, [exactAmount, minAmount, maxAmount]);

  const logicSetMode = (mode) => {
    switch (mode) {
      case "none":
        setExactAmount(0);
        setMinAmount(0);
        setMaxAmount(0);
        break;
      case "exact":
        setMinAmount(0);
        setMaxAmount(0);
        break;
      case "min-max":
        setExactAmount(0);
        break;
    }
    setMode(mode);
  }

  return <div className="target-editor">
    <div className="tabs">
      <button type="button" title="No target" onClick={() => logicSetMode("none")} className={`${mode==="none"?"selected":""}`}>
        <i className='bx bx-x'></i>
      </button>
      <button type="button" title="Exact target" onClick={() => logicSetMode("exact")} className={`${mode==="exact"?"selected":""}`}>
        <i className='bx bx-arrow-to-right right-one'></i>
        <i className='bx bx-arrow-to-left left-one'></i>
      </button>
      <button type="button" title="Min-max target" onClick={() => logicSetMode("min-max")} className={`${mode==="min-max"?"selected":""}`}>
        <i className='bx bx-arrow-to-left right-one'></i>
        <i className='bx bx-arrow-to-right left-one'></i>
      </button>
    </div>
    <div hidden={mode!=="none"} className="tab-content">
      <div className="visualisation">
        <div className="visualisation-line">
          <div className="blue"></div>
        </div>
      </div>
      <div className="target-values">
        <div>No target set</div>
      </div>
    </div>
    <div hidden={mode!=="exact"} className="tab-content">
      <div className="visualisation">
        <div className="visualisation-line">
          <div className={`${exactAmount>0?"gray":"blue"}`}></div>
        </div>
        <div className="visualisation-icon">
          <i title="Exact" className={`bx bxs-location-plus bx-flip-vertical ${exactAmount>0?"blue":"gray"}`}></i>
        </div>
      </div>
      <div className="target-values">
        <input type="number" className="amount-field" min="0" step="0.001" onChange={e => {setExactAmount(Number(e.target.value))}} value={exactAmount}/>
      </div>
    </div>
    <div hidden={mode!=="min-max"} className="tab-content">
      <div className="visualisation">
        <div className="visualisation-line">
          <div className={`${minAmount>0?"gray":"blue"}`}></div>
          <div className="blue"></div>
          <div className={`${maxAmount>0?"gray":"blue"}`}></div>
        </div>
        <div className="visualisation-icon">
          <i title="Minimum" className={`bx bxs-location-plus bx-flip-vertical ${minAmount>0?"red":"gray"}`}></i>
          <i title="Maximum" className={`bx bxs-location-plus bx-flip-vertical ${maxAmount>0?"green":"gray"}`}></i>
        </div>
      </div>
      <div className="target-values">
        <input type="number" className="amount-field" min="0" step="0.001" onChange={e => {setMinAmount(Number(e.target.value))}} value={minAmount}/>
        <input type="number" className="amount-field" min="0" step="0.001" onChange={e => {setMaxAmount(Number(e.target.value))}} value={maxAmount}/>
      </div>
    </div>
  </div>;
}

function getInitialTargetValue(targets, type) {
  const t = targets.find(v => v.type === type);
  return t ? t.amount : 0;
}

function checkTargetListCopy(newList, oldList) {
  if (newList.length !== oldList.length) return false;
  
  return newList.every((v, i) => {
    const v2 = oldList[i];
    return v.amount === v2.amount && v.type === v2.type;
  })
}
