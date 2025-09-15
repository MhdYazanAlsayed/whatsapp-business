import Entity from "../../../base/Entity";
import TemplateComponentId from "../keys/TemplateComponentId";
import TemplateComponent from "../TemplateComponent";
import TemplateVariableId from "./keys/TemplateVariableId";

export default interface TemplateVariable extends Entity {
  id: TemplateVariableId;
  key: string;
  value: string;
  componentId: TemplateComponentId;
  component?: TemplateComponent;
}
