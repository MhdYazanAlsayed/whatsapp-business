import IRequest from "src/app/core/helpers/mediatR/IRequest";
import CreateTemplatePayload from "./CreateTemplatePayload";
import Template from "src/app/core/entities/templates/Template";
import { ResultDto } from "src/app/core/helpers/TaskResults";
export default class CreateTemplateCommand extends IRequest<
  ResultDto<Template>
> {
  constructor(readonly payload: CreateTemplatePayload) {
    super();
  }
}
