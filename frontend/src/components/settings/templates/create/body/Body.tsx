import { useEvents } from "src/hooks/useEvents";
import Props from "./Props";
import { useRef } from "react";

const Body = ({ direction, bodyComponent, setBodyComponent }: Props) => {
  const { handleOnChange } = useEvents(setBodyComponent);

  const ref = useRef<HTMLTextAreaElement>(null);

  const handleAddVariable = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();

    setBodyComponent((x) => ({
      ...x,
      text: x.text + " {{" + (x.example.bodyText.length + 1) + "}} ",
      example: {
        ...x.example,
        bodyText: [...x.example.bodyText, ""],
      },
    }));
    ref.current?.focus();
  };

  const handleChangeVariable = (index: number, value: string) => {
    setBodyComponent((x) => {
      x.example.bodyText[index] = value;
      return { ...x };
    });
  };

  const handleRemoveVariable = (e: React.MouseEvent<HTMLElement>, index: number) => {
    e.preventDefault();

    setBodyComponent((x) => {
      x.example.bodyText.splice(index, 1);

      // Delete current variable
      let text = x.text.replace(
        new RegExp(` \\{\\{${index + 1}\\}\\} `, "g"),
        ""
      );

      // Get all variables
      let variables = text.split("}}");

      // This to remove the other text
      variables.pop();

      // This to replace the variables with the new ones
      let counter = 1;
      for (let variable of variables) {
        text = text.replace(
          new RegExp(
            `\\{\\{${variable.slice(variable.indexOf("{{") + 2)}\\}\\}`,
            "g"
          ),
          "{{" + counter + "}}"
        );
        counter++;
      }

      return {
        ...x,
        text: text,
        example: x.example,
      };
    });
  };

  return (
    <div className="mb-3">
      <h5 className="py-3 mb-3 text-blue fw-semibold">جسم القالب</h5>

      <div className="col-md-12 mb-3">
        <div className="form-group">
          <label htmlFor="body" className="mb-1 text-muted fw-semibold">
            النص
            <span className="text-danger">*</span>
          </label>
          <textarea
            ref={ref}
            dir={direction}
            className="form-control"
            id="body"
            rows={3}
            value={bodyComponent.text}
            onChange={(e) => handleOnChange("text", e.target.value)}
          />
        </div>
      </div>

      <div className="row">
        {bodyComponent.example.bodyText.map((example, index) => (
          <div className="col-md-6 mb-3" key={index}>
            <div className="form-group">
              <label htmlFor="example" className="mb-1 text-muted fw-semibold">
                المتغيير {index + 1}
              </label>
              <div className="d-flex align-items-center gap-2">
                <input
                  className="form-control"
                  value={example}
                  onChange={(e) => handleChangeVariable(index, e.target.value)}
                />

                <i
                  className="fa fa-trash text-danger cursor-pointer"
                  onClick={(e) => handleRemoveVariable(e, index)}
                ></i>
              </div>
            </div>
          </div>
        ))}
      </div>

      <button
        className="custom-btn rounded primary"
        onClick={handleAddVariable}
      >
        اضافة متغير
      </button>
    </div>
  );
};

export default Body;
