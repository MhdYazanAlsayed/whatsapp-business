import { useRef, useState } from "react";
import { Fragment } from "react/jsx-runtime";
import Props from "./Props";

const HeaderTextEditor = ({
  direction,
  headerComponent,
  setHeaderComponent,
}: Props) => {
  const ref = useRef<HTMLTextAreaElement | null>(null);
  const [formData, setFormData] = useState("");

  const handleAddVariable = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();

    setHeaderComponent((x) => ({
      ...x,
      text: x.text + " {{1}} ",
      example: {
        ...x.example,
        headerText: [""],
      },
    }));

    ref.current?.focus();
  };

  const handleRemoveVariable = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();

    setHeaderComponent((x) => ({
      ...x,
      text: x.text.replace(/\{\{1\}\}/g, ""),
      example: {
        ...x.example,
        headerText: [],
      },
    }));

    setFormData("");
    ref.current?.focus();
  };

  const handleReuseVariable = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();

    setHeaderComponent((x) => ({
      ...x,
      text: x.text + " {{1}} ",
    }));

    ref.current?.focus();
  };

  const handleChangeExample = (value: string) => {
    setFormData(value);

    setHeaderComponent((x) => {
      x.example.headerText = [value];

      return { ...x };
    });
  };

  return (
    <Fragment>
      <div className="col-md-12 mb-3">
        <div className="form-group">
          <label htmlFor="text" className="mb-1 text-muted fw-semibold">
            النص
            <span className="text-danger">*</span>
          </label>
          <textarea
            ref={ref}
            dir={direction}
            className="form-control"
            id="text"
            rows={3}
            value={headerComponent.text}
            onChange={(e) =>
              setHeaderComponent({
                ...headerComponent,
                text: e.target.value,
              })
            }
          />
        </div>
      </div>

      {headerComponent.example.headerText.length > 0 && (
        <div className="col-md-6 mb-3">
          <div className="form-group mb-3">
            <label className="mb-1">المتغير</label>
            <div className="d-flex align-items-center gap-2">
              <input
                className="form-control"
                value={formData}
                onChange={(e) => handleChangeExample(e.target.value)}
              />

              <i
                className="fa-solid fa-trash-can text-danger cursor-pointer"
                onClick={handleRemoveVariable}
              ></i>

              <i
                className="fa-solid fa-arrows-rotate text-blue cursor-pointer"
                onClick={handleReuseVariable}
              ></i>
            </div>
          </div>
        </div>
      )}

      <button
        className="custom-btn rounded primary"
        onClick={handleAddVariable}
        disabled={headerComponent.example.headerText.length > 0}
      >
        اضافة متغيير
      </button>
    </Fragment>
  );
};

export default HeaderTextEditor;
