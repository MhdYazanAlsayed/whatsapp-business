import { TemplateComponentFormat } from "src/app/core/entities/templates/template-components/enums/TemplateComponentFormat";
import { FormData } from "./Props";
import { TemplateCategory } from "src/app/core/entities/templates/enums/TemplateCategory";
import { TemplateLanguage } from "src/app/core/entities/templates/enums/TemplateLanguage";
import { TemplateButtonPayload } from "src/app/features/templates/commands/create/CreateTemplatePayload";
import { TemplateButtonType } from "src/app/core/entities/templates/enums/TemplateButtonType";

interface SubmitButtonProps {
  headerComponent: FormData;
  bodyComponent: FormData;
  footerComponent: string;
  buttons: TemplateButtonPayload[];
  formData: {
    name: string;
    language: TemplateLanguage;
    category: TemplateCategory;
  };
}

const SubmitButton = ({
  headerComponent,
  bodyComponent,
  formData,
  footerComponent,
  buttons,
}: SubmitButtonProps) => {
  const isHeaderDoesNotValid =
    headerComponent.format === -1
      ? false
      : headerComponent.format === TemplateComponentFormat.Text
      ? headerComponent.text?.trim() == "" ||
        (headerComponent.example.headerText.length > 0 &&
          headerComponent.example.headerText.some(
            (example) => example.trim() === ""
          ))
      : headerComponent.example.headerHandle == null;

  const isBodyDoesNotValid =
    bodyComponent.text.trim() === "" ||
    (bodyComponent.example.bodyText.length > 0 &&
      bodyComponent.example.bodyText.some((example) => example.trim() === ""));

  const isFooterDoesNotValid = footerComponent.trim() === "";

  const isButtonsNotValid = buttons.some(
    (button) =>
      (button.type == TemplateButtonType.Url &&
        (!button.url || button.url.trim() == "")) ||
      (button.type == TemplateButtonType.PhoneNumber &&
        (!button.phoneNumber || button.phoneNumber.trim() == "")) ||
      !button.text ||
      button.text.trim() == ""
  );

  return (
    <button
      className="custom-btn rounded primary"
      type="submit"
      disabled={
        formData.name.trim() === "" ||
        isHeaderDoesNotValid ||
        isBodyDoesNotValid ||
        isFooterDoesNotValid ||
        isButtonsNotValid
      }
    >
      انشاء قالب
    </button>
  );
};

export default SubmitButton;
