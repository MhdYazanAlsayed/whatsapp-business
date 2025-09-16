import IRequest from "src/app/core/helpers/app_helpers/IRequest";
import CreateReplyTemplateCommandPayload from "./CreateReplyTemplateCommandPayload";
import ReplyTemplate from "src/app/core/entities/reply-templates/ReplyTemplate";

export default class CreateReplyTemplateCommand extends IRequest<ReplyTemplate> {
  constructor(readonly payload: CreateReplyTemplateCommandPayload) {
    super();
  }
}
