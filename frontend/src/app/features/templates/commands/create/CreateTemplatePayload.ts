import { TemplateButtonType } from "src/app/core/entities/templates/enums/TemplateButtonType";
import { TemplateCategory } from "src/app/core/entities/templates/enums/TemplateCategory";
import { TemplateLanguage } from "src/app/core/entities/templates/enums/TemplateLanguage";
import { TemplateComponentFormat } from "src/app/core/entities/templates/template-components/enums/TemplateComponentFormat";
import { TemplateComponentType } from "src/app/core/entities/templates/template-components/enums/TemplateComponentType";

export default interface CreateTemplatePayload {
  name: string;
  language: TemplateLanguage;
  category: TemplateCategory;
  header?: TemplateComponentPayload;
  body: TemplateComponentPayload;
  footer?: TemplateComponentPayload;
  buttons?: TemplateComponentPayload;
}

export interface TemplateComponentPayload {
  type: TemplateComponentType;
  text?: string;
  format?: TemplateComponentFormat;
  example?: TemplateComponentExamplePayload;
  buttons?: TemplateButtonPayload[];
}

export interface TemplateComponentExamplePayload {
  headerText?: string;
  headerHandle?: string;
  bodyText?: string[];
  footerText?: string[];
}

export interface TemplateButtonPayload {
  type: TemplateButtonType;
  text?: string;
  url?: string;
  phoneNumber?: string;
}

// export interface TemplateComponent {
//   type: TemplateComponentType;
//   text?: string;
//   format?: TemplateComponentFormat;
//   example?: string[];
// }
