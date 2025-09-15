import IRequest from "src/app/core/helpers/mediatR/IRequest";
import UploadNoteAttachmentCommandPayload from "./UploadNoteAttachmentCommandPayload";
import UploadNoteAttachmentResult from "./UploadNoteAttachmentResult";

export default class UploadNoteAttachmentCommand extends IRequest<UploadNoteAttachmentResult> {
  constructor(readonly payload: UploadNoteAttachmentCommandPayload) {
    super();
  }
}
