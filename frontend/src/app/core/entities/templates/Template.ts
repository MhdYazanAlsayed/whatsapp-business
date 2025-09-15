import Entity from "../base/Entity";
import { TemplateLanguage } from "./enums/TemplateLanguage";
import TemplateId from "./keys/TemplateId";
import TemplateButton from "./template-buttons/TemplateButton";
import TemplateComponent from "./template-components/TemplateComponent";
import { TemplateStatus } from "./enums/TemplateStatus";
import { TemplateCategory } from "./enums/TemplateCategory";

export default interface Template extends Entity {
  id: TemplateId;
  name: string;
  status: TemplateStatus;
  category: TemplateCategory;
  subCategory: string;
  language: TemplateLanguage;
  components: TemplateComponent[];
  buttons: TemplateButton[];
}
