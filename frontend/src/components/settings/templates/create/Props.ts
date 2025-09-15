import { TemplateComponentFormat } from "src/app/core/entities/templates/template-components/enums/TemplateComponentFormat";
import { TemplateComponentType } from "src/app/core/entities/templates/template-components/enums/TemplateComponentType";

export interface FormData {
  type: TemplateComponentType;
  text: string;
  format: TemplateComponentFormat | -1;
  example: FormDataExample;
}

export interface FormDataExample {
  headerText: string[];
  headerHandle?: File;
  bodyText: string[];
  footerText: string[];
}
