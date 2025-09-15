import { Fragment } from "react";
import Props from "./props/Props";

const HeaderModal = ({ handleSyncTemplatesAsync }: Props) => {
  return (
    <Fragment>
      <div className="d-flex align-items-center justify-content-between">
        <p className="fw-semibold mb-3">اختر قالبا جاهزا من هنا</p>
        <span
          className="text-success cursor-pointer"
          onClick={handleSyncTemplatesAsync}
        >
          <i className="fa-solid fa-sync"></i>
        </span>
      </div>
      <div className="tabs">
        {/* <button className="active">الردود الجاهزة</button> */}
        <button className="active">القوالب</button>
      </div>
    </Fragment>
  );
};

export default HeaderModal;
