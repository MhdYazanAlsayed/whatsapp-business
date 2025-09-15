import Entity from "../base/Entity";
import ReplyTemplateId from "./keys/ReplyTemplateId";

export default interface ReplyTemplate extends Entity {
  id: ReplyTemplateId;
  content: string;
  title: string;
}
