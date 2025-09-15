import Entity from "../../base/Entity";
import TemplateMessageId from "./keys/TemplateMessageId";

export default interface TemplateMessage extends Entity {
  id: TemplateMessageId;
  headerFileName?: string;
  body: string;
  footer?: string;
}
