import IRequest from "src/app/core/helpers/mediatR/IRequest";
import { ChangeEmployeeStatusPayload } from "./ChangeEmployeeStatusPayload";
import { AccountStatusResponse } from "./AccountStatusResponse";

export default class ChangeEmployeeStatusCommand extends IRequest<AccountStatusResponse> {
  constructor(public readonly payload: ChangeEmployeeStatusPayload) {
    super();
  }
}
