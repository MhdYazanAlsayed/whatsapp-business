import { TemplateBodyFormData } from "../../../types/TemplateFormData";
import ComponentProps from "../../props/ComponentProps";

export default interface Props extends ComponentProps {
  formData: TemplateBodyFormData[];
  handleAddBodyParameterValue(data: TemplateBodyFormData): void;
}
