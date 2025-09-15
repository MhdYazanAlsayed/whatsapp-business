import { TemplateBodyFormData } from "../../../../types/TemplateFormData";

export default interface Props {
  text: string;
  value?: string;
  number: number;
  handleAddBodyParameterValue(data: TemplateBodyFormData): void;
}
