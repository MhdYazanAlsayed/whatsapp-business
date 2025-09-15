import IRequest from "src/app/core/helpers/mediatR/IRequest";
import { SimpleResultDto } from "src/app/core/helpers/TaskResults";

export default class GetAccountStatusCommand extends IRequest<SimpleResultDto> {
  constructor() {
    super();
  }
}
