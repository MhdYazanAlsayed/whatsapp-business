export default interface TemplateFormData {
  header: TemplateHeaderFormData;
  body: TemplateBodyFormData[];
}

export interface TemplateBodyFormData {
  value: string;
  number: number;
}

export interface TemplateHeaderFormData {
  file: File | null;
}
