import "./Popup.scss";
import React, {ReactNode, useEffect, useRef} from "react";

export type PopupProps = {
  hidden: boolean,
  children: ReactNode[] | ReactNode,
  onClose: () => void
}

export function Popup({hidden, children, onClose}: PopupProps): React.JSX.Element {
  const popupRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (popupRef.current === null) return;
    const container = popupRef.current
    container.addEventListener("closePopup", onClose);
    return () => container.removeEventListener("closePopup", onClose);
  }, [popupRef, onClose])

  return (
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
