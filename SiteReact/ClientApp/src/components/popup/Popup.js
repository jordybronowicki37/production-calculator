import "./Popup.css";

export function Popup({hidden, children, onClose}) {
  return(
    <div hidden={hidden} className="popup-container" onClick={onClose}>
      <div onClick={e => e.stopPropagation()}>
        <button type="button" className="popup-close-button" onClick={onClose}>
          <i className='bx bx-x'></i>
        </button>
        {children}
      </div>
    </div>
  );
}
