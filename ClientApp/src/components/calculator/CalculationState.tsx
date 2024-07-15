import "./CalculationState.scss";
import {useEffect, useState} from "react";
// @ts-ignore idk why the import requires a .d.ts
import {AlertLevel, Alert} from "../../data/DataTypes.d.ts";

export type CalculationStateProps = {
  alerts: Alert[],
  onClick: () => Promise<any>,
}

export function CalculationState({alerts, onClick}: CalculationStateProps) {
  const [isLoading, setIsLoading] = useState(false);
  const [isFailed, setIsFailed] = useState(false);
  const [amountOfWarnings, setAmountOfWarnings] = useState(0);
  const [amountOfErrors, setAmountOfErrors] = useState(0);

  useEffect(() => {
    setIsLoading(false);
    setIsFailed(false);
    setAmountOfWarnings(alerts.filter(a => a.level === AlertLevel.Warning).length)
    setAmountOfErrors(alerts.filter(a => a.level === AlertLevel.Error).length)
  }, [alerts]);
  
  const onClickWrapper = () => {
    setIsLoading(true);
    onClick()
      .then(() => setIsLoading(false))
      .catch(() => setIsFailed(true));
  };

  return (
    <div className="calculation-state">
      <div className="calculation-info">
        <div title="Calculation success" hidden={amountOfWarnings != 0 || amountOfErrors != 0 || isLoading} >
          <i className='bx bx-check'/>
        </div>
        <div hidden={amountOfWarnings == 0} title="Calculation had warnings">
          <div>{amountOfWarnings}</div>
          <i className='bx bx-error-circle' style={{color: "#F1B22C"}} />
        </div>
        <div hidden={amountOfErrors == 0} title="Calculation had errors">
          <div>{amountOfErrors}</div>
          <i className='bx bx-error' style={{color: "#F12C2C"}} />
        </div>
      </div>

      <div className="recalculate-icon-wrapper" onClick={onClickWrapper}>
        <i hidden={isLoading} title="Recalculate" className='bx bx-refresh'/>
        <i hidden={!isLoading} title="Calculating..." className='bx bx-loader-alt bx-spin'/>
        <i hidden={!isFailed} title="Something went wrong" className='bx bx-error' style={{color: "#F12C2C"}}/>
      </div>
    </div>
  );
}