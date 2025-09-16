import IRequest from "src/app/core/helpers/app_helpers/IRequest";
import ValidateCodeCommandPayload from "./ValidateCodeCommandPayload";
import ValidateCodeResponse from "./ValidateCodeResponse";

export default class ValidateCodeCommand extends IRequest<ValidateCodeResponse> {
  constructor(readonly payload: ValidateCodeCommandPayload) {
    super();
  }
}
