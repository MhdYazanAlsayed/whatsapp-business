import Entity from "../../base/Entity";
import { TemplateButtonType } from "../enums/TemplateButtonType";
import TemplateId from "../keys/TemplateId";
import Template from "../Template";
import TemplateButtonId from "./keys/TemplateButtonId";

export default interface TemplateButton extends Entity {
  id: TemplateButtonId;
  url?: string;
  type: TemplateButtonType;
  text?: string;
  templateId: TemplateId;
  template?: Template;
}
