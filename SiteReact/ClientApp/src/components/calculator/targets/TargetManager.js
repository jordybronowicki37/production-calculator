﻿import "./TargetManager.scss"
import {useEffect, useState} from "react";
import {setTargets} from "../../../data/api/TargetAPI";
import {nanoid} from "@reduxjs/toolkit";

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
  
  const exactInputId = nanoid();
  const minInputId = nanoid();
  const maxInputId = nanoid();

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
    <div className="target-editor-content-wrapper">
      <div className="tabs">
        <button type="button" title="No target" onClick={() => logicSetMode("none")} className={`${mode==="none"?"selected":""}`}>
          None
        </button>
        <button type="button" title="Exact target" onClick={() => logicSetMode("exact")} className={`${mode==="exact"?"selected":""}`}>
          Exact
        </button>
        <button type="button" title="Min-max target" onClick={() => logicSetMode("min-max")} className={`${mode==="min-max"?"selected":""}`}>
          Min-Max
        </button>
      </div>

      <div className="tab-content">
        {mode === "none" &&
          <span>No target set</span>
        }

        {mode === "exact" &&
          <div>
            <label htmlFor={exactInputId}>Exact amount</label>
            <input id={exactInputId} type="number" className="amount-field" min="0" step="0.001" onChange={e => {setExactAmount(Number(e.target.value))}} value={exactAmount}/>
          </div>
        }

        {mode === "min-max" &&
          <>
            <div>
              <label htmlFor={minInputId}>Min amount</label>
              <input id={minInputId} type="number" className="amount-field" min="0" step="0.001" onChange={e => {setMinAmount(Number(e.target.value))}} value={minAmount}/>
            </div>
            <div>
              <label htmlFor={maxInputId}>Max amount</label>
              <input id={maxInputId} type="number" className="amount-field" min="0" step="0.001" onChange={e => {setMaxAmount(Number(e.target.value))}} value={maxAmount}/>
            </div>
          </>
        }
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
