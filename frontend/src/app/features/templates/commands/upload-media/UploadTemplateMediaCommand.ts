import IRequest from "src/app/core/helpers/app_helpers/IRequest";
import UploadTemplateMediaCommandPayload from "./UploadTemplateMediaCommandPayload";
import UploadTemplateMediaCommandResult from "./UploadTemplateMediaCommandResult";

export default class UploadTemplateMediaCommand extends IRequest<UploadTemplateMediaCommandResult> {
  constructor(readonly payload: UploadTemplateMediaCommandPayload) {
    super();
  }
}
