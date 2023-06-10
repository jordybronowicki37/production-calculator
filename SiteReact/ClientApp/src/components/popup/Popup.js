import "./Popup.css";
import {useEffect, useRef} from "react";

export function Popup({hidden, children, onClose}) {
  const popupRef = useRef(null);
  
  useEffect(() => {
    if (popupRef.current === null) return;
    const container = popupRef.current
    container.addEventListener("closePopup", onClose);
    return () => container.removeEventListener("closePopup", onClose);
  }, [popupRef.current])
  
  return(
    <div hidden={hidden} className="popup-container" onClick={onClose}>
      <div onClick={e => e.stopPropagation()} ref={popupRef}>
        <button type="button" className="popup-close-button" onClick={onClose}>
          <i className='bx bx-x'></i>
        </button>
        {!hidden && children}
      </div>
    </div>
  );
}
