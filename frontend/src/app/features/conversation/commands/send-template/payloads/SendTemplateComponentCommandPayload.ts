import { SendTemplateParameterCommandPayload } from "./SendTemplateParameterCommandPayload";
import { TemplateComponentType } from "src/app/core/entities/templates/template-components/enums/TemplateComponentType";

export interface SendTemplateComponentCommandPayload {
  type: TemplateComponentType;
  parameters: SendTemplateParameterCommandPayload[];
}
