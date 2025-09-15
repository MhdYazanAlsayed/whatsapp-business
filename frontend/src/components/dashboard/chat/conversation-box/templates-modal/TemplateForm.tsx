import { Fragment } from "react";
import TemplateFormProps from "./props/TemplateFormProps";
import { TemplateComponentType } from "src/app/core/entities/templates/template-components/enums/TemplateComponentType";
import TemplateHeader from "./template-components/header/TemplateHeader";
import TemplateBody from "./template-components/body/TemplateBody";
import { TemplateLanguage } from "src/app/core/entities/templates/enums/TemplateLanguage";

const TemplateForm = ({
  template,
  formData,
  handleAddBodyParameterValue,
  handleAddHeaderParameterValue,
}: TemplateFormProps) => {
  if (!template) {
    return (
      <div
        className="p-2"
        style={{
          borderRadius: "0.75rem",
          backgroundColor: "var(--background-color)",
          flex: "1",
        }}
      ></div>
    );
  }

  const isRTL = template.language === TemplateLanguage.Arabic;
  const header = template.components.find(
    (x) => x.type == TemplateComponentType.Header
  );
  const body = template.components.find(
    (x) => x.type == TemplateComponentType.Body
  );
  // const footer = template.components.find(
  //   (x) => x.type.toUpperCase() === TemplateComponentType.Footer
  // );

  return (
    <Fragment>
      <div
        className="p-2"
        style={{
          borderRadius: "0.75rem",
          backgroundColor: "var(--background-color)",
          flex: "1",
        }}
      >
        <div className="template-message p-3">
          <TemplateHeader
            data={header!}
            isRTL={isRTL}
            handleAddHeaderParameterValue={handleAddHeaderParameterValue}
            fileName={formData.header.file?.name ?? ""}
          />
          <TemplateBody
            data={body!}
            isRTL={isRTL}
            formData={formData.body}
            handleAddBodyParameterValue={handleAddBodyParameterValue}
          />
        </div>
      </div>
    </Fragment>
  );
};

export default TemplateForm;
