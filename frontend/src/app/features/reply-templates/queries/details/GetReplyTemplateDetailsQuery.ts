import IRequest from "src/app/core/helpers/mediatR/IRequest";
import GetReplyTemplateDetailsQueryPayload from "./GetReplyTemplateDetailsQueryPayload";
import ReplyTemplate from "src/app/core/entities/reply-templates/ReplyTemplate";

export default class GetReplyTemplateDetailsQuery extends IRequest<ReplyTemplate | null> {
  constructor(readonly payload: GetReplyTemplateDetailsQueryPayload) {
    super();
  }
}
