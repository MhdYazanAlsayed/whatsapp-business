import IRequest from "src/app/core/helpers/mediatR/IRequest";
import UploadTemplateMediaCommandPayload from "./UploadTemplateMediaCommandPayload";
import UploadTemplateMediaCommandResult from "./UploadTemplateMediaCommandResult";

export default class UploadTemplateMediaCommand extends IRequest<UploadTemplateMediaCommandResult> {
  constructor(readonly payload: UploadTemplateMediaCommandPayload) {
    super();
  }
}
