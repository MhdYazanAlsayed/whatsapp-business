import Entity from "../../base/Entity";
import TemplateId from "../keys/TemplateId";
import Template from "../Template";
import { TemplateComponentFormat } from "./enums/TemplateComponentFormat";
import { TemplateComponentType } from "./enums/TemplateComponentType";
import TemplateComponentId from "./keys/TemplateComponentId";
import TemplateVariable from "./template-variables/TemplateVariable";

export default interface TemplateComponent extends Entity {
  id: TemplateComponentId;
  type: TemplateComponentType;
  text: string;
  format: TemplateComponentFormat;
  templateId: TemplateId;
  template?: Template;
  variables: TemplateVariable[];
}
