import "./CalculationState.scss";

export function CalculationState({message, state, onClick}) {
  return (
    <div className="calculation-states">
      <div hidden={message===""} className="calculation-states-label">{message}</div>
      <div className="calculation-states-icon" onClick={onClick}>
        <i hidden={state !== "success"} title="Calculation success" className='bx bx-check'></i>
        <i hidden={state !== "warning"} title="Calculation was unsuccessful" className='bx bx-error' style={{color:"#F12C2C"}}></i>
        <i hidden={state !== "error"} title="Calculation had error's" className='bx bx-error-circle' style={{color:"#F1B22C"}}></i>
        <i hidden={state !== "loading"} title="Calculating..." className='bx bx-loader-alt bx-spin'></i>
        <i hidden={state !== "refresh"} title="Recalculate" className='bx bx-refresh'></i>
      </div>
    </div>
  );
}