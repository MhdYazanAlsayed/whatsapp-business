import { FormEvent, useState } from "react";
import Props from "./props/Props";

const TemplateVariable = ({
  text,
  value,
  handleAddBodyParameterValue,
  number,
}: Props) => {
  const [status, setStatus] = useState(false);
  const [formData, setFormData] = useState(value ?? "");

  const handleOnSaveValue = (e: FormEvent<HTMLButtonElement>) => {
    e.preventDefault();

    handleAddBodyParameterValue({ number: number, value: formData });
    setStatus(false);
  };

  return (
    <div className="template-variable">
      <button
        className="btn btn-primary py-0 px-1 mt-1 mb-1"
        key={Math.random()}
        onClick={() => setStatus((x) => !x)}
      >
        {value ?? text}
      </button>

      <div
        className="menu rounded shadow border"
        dir="rtl"
        data-status={status ? "opened" : "closed"}
      >
        <div className="p-2">
          <p className="d-block mb-2">قيمة المتغير</p>
          <div className="d-flex align-items-center gap-2">
            <input
              className="form-control py-0"
              value={formData}
              onChange={(x) => setFormData(x.currentTarget.value)}
            />
            <button
              className="btn btn-primary btn-sm "
              style={{ paddingBlock: "1px" }}
              onClick={handleOnSaveValue}
            >
              حفظ
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default TemplateVariable;
