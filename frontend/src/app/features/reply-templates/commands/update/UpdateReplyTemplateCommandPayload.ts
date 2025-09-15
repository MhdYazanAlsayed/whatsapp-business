import ReplyTemplateId from "src/app/core/entities/reply-templates/keys/ReplyTemplateId";

export default interface UpdateReplyTemplateCommandPayload {
  id: ReplyTemplateId;
  title: string;
  content: string;
}
