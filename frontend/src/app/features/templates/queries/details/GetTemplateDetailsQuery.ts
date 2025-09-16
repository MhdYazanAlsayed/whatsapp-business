// GetTemplateDetailsQuery.ts

import IRequest from "src/app/core/helpers/app_helpers/IRequest";
import { GetTemplateDetailsQueryPayload } from "./GetTemplateDetailsQueryPayload";
import Template from "src/app/core/entities/templates/Template";

export default class GetTemplateDetailsQuery extends IRequest<Template | null> {
  constructor(readonly payload: GetTemplateDetailsQueryPayload) {
    super();
  }
}
