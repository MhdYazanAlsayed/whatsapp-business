import { TemplateComponentFormat } from "src/app/core/entities/templates/template-components/enums/TemplateComponentFormat";
import { Fragment } from "react/jsx-runtime";
import { memo } from "react";
import Props from "./props/Props";
import TemplateVariable from "./variable/TemplateVariable";

const TemplateBody = ({
  data,
  isRTL,
  formData,
  handleAddBodyParameterValue,
}: Props) => {
  if (!data) return null;

  const { format, text } = data;

  // Slice text and replace it to html
  let result: any = [];
  var content = text;
  let count = 0;

  // ? This code to convert the string to html code in order to make the variables as buttons
  while (true) {
    if (count == 30) {
      console.error("Infinit loop has been breaked");
      break;
    }

    if (content.length == 0) break;

    const startOfVariableIndex = content.indexOf("{{");
    if (startOfVariableIndex == -1) {
      // There is no variables
      result.push(<span key={Math.random()}>{content}</span>);

      content = "";
      break;
    }

    var previousText = content.slice(0, startOfVariableIndex);
    if (previousText.length != 0) {
      result.push(<span key={Math.random()}>{previousText}</span>); // Take the text
      content = content.slice(startOfVariableIndex); // Update the whole text
    }

    const endOfVariableIndex = content.indexOf("}}") + 2;
    const variable = content.slice(0, endOfVariableIndex);
    // Push the variable on results
    result.push(
      <TemplateVariable
        text={variable}
        value={formData.find((x) => x.number == count)?.value}
        handleAddBodyParameterValue={handleAddBodyParameterValue}
        number={count}
        key={count}
      />
    );

    content = content.slice(endOfVariableIndex);
    count++;
  }

  if (format == null || format === TemplateComponentFormat.Text) {
    // Replace variables to buttons
    return (
      <Fragment>
        <div className="template-body mt-2" dir={isRTL ? "rtl" : "ltr"}>
          {result}
        </div>
      </Fragment>
    );
  }

  throw new Error("Not implemented template body component format .");
};

export default memo(TemplateBody);
