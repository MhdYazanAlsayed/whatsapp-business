import { useState, useEffect } from "react";
import ModalProps from "./props/ModalProps";

const TadawiModal = ({
  className,
  open,
  setOpen,
  width,
  children,
}: ModalProps) => {
  const [show, setIsShow] = useState(open);
  const [resultState, setResultState] = useState(open);

  useEffect(() => {
    if (open) {
      setIsShow(true);
      setResultState(true);
    } else handleOnClose(null);
  }, [open]);

  const handleOnClose = <T,>(e: React.FormEvent<T> | null) => {
    e?.preventDefault();
    setIsShow(false);
    setOpen(false);
    setTimeout(() => {
      setResultState(false);
    }, 300);
  };

  return !resultState ? null : (
    <div
      className={`custom-modal ${className}`}
      data-status={show ? "opened" : "closed"}
    >
      <div className="overlay" onClick={() => setOpen((o) => !o)}></div>
      <div className="modal-form" style={{ width: width }}>
        {/* <div className="form-body"></div> */}
        {children}
      </div>
    </div>
  );
};

export default TadawiModal;
