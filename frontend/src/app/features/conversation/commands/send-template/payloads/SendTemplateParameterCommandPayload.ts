import { TemplateParameterType } from "../enums/TemplateParameterType";
import { SendTemplateParameterFileCommandPayload } from "./SendTemplateParameterFileCommandPayload";

export interface SendTemplateParameterCommandPayload {
  type: TemplateParameterType;
  text?: string;
  document?: SendTemplateParameterFileCommandPayload;
  image?: SendTemplateParameterFileCommandPayload;
  video?: SendTemplateParameterFileCommandPayload;
}
