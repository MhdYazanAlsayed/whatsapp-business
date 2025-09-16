import IRequest from "src/app/core/helpers/app_helpers/IRequest";
import GetTemplatesPaginationPayload from "./GetTemplatesPaginationPayload";
import Template from "src/app/core/entities/templates/Template";
import PaginationDto from "src/app/core/helpers/PaginationDto";

export default class GetTemplatesPaginationQuery extends IRequest<
  PaginationDto<Template[]>
> {
  constructor(readonly payload: GetTemplatesPaginationPayload) {
    super();
  }
}
