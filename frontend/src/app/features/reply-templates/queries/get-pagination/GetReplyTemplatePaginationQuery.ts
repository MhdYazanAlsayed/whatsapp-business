import IRequest from "src/app/core/helpers/mediatR/IRequest";
import GetReplyTemplatePaginationQueryPayload from "./GetReplyTemplatePaginationQueryPayload";
import ReplyTemplate from "src/app/core/entities/reply-templates/ReplyTemplate";
import PaginationDto from "src/app/core/helpers/PaginationDto";

export default class GetReplyTemplatePaginationQuery extends IRequest<
  PaginationDto<ReplyTemplate[]>
> {
  constructor(readonly payload: GetReplyTemplatePaginationQueryPayload) {
    super();
  }
}
