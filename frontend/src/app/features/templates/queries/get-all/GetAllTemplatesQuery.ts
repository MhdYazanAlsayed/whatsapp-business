import Template from "src/app/core/entities/templates/Template";
import IRequest from "src/app/core/helpers/app_helpers/IRequest";

export default class GetAllTemplateQuery extends IRequest<Template[]> {
  constructor() {
    // يمكنك إضافة أي معايير أو فلترة في المستقبل
    super();
  }
}
