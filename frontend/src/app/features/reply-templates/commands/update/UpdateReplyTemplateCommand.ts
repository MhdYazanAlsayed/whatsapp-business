import IRequest from "src/app/core/helpers/app_helpers/IRequest";
import UpdateReplyTemplateCommandPayload from "./UpdateReplyTemplateCommandPayload";
import ReplyTemplate from "src/app/core/entities/reply-templates/ReplyTemplate";

export default class UpdateReplyTemplateCommand extends IRequest<ReplyTemplate> {
  constructor(readonly payload: UpdateReplyTemplateCommandPayload) {
    super();
  }
}
