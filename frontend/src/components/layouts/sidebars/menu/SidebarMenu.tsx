import { useState } from "react";
import Props from "./props/Props";

const SidebarMenu = ({ text, icon, children }: Props) => {
  const [status, setStatus] = useState(false);

  return (
    <div className="item primary">
      <a
        href="#"
        onClick={(x) => {
          x.preventDefault();

          setStatus((a) => !a);
        }}
        className="link d-flex align-items-center gap-2 py-1 px-2 rounded mb-2"
      >
        {icon}

        <span>{text}</span>
        <i className="fa-solid fa-chevron-down me-auto"></i>
      </a>

      <div
        className="menu d-flex flex-column gap-2"
        data-status={status ? "opened" : "closed"}
      >
        {children}
      </div>
    </div>
  );
};

export default SidebarMenu;
