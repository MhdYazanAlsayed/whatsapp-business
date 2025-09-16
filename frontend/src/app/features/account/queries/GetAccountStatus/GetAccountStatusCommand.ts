import IRequest from "src/app/core/helpers/app_helpers/IRequest";
import { SimpleResultDto } from "src/app/core/helpers/TaskResults";

export default class GetAccountStatusCommand extends IRequest<SimpleResultDto> {
  constructor() {
    super();
  }
}
