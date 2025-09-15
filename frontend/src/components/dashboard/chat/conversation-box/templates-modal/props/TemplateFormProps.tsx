import Template from "src/app/core/entities/templates/Template";
import TemplateFormData, {
  TemplateBodyFormData,
} from "../types/TemplateFormData";

export default interface TemplateFormProps {
  template: Template | null;
  formData: TemplateFormData;
  handleAddBodyParameterValue(data: TemplateBodyFormData): void;
  handleAddHeaderParameterValue(file: File | null): void;
}
