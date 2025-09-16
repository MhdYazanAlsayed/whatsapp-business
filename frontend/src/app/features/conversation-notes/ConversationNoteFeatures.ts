import FeatureList from "src/app/core/helpers/app_helpers/types/FeatureList";
import UploadNoteAttachmentCommand from "./commands/upload-attachments/UploadNoteAttachmentCommand";
import UploadNoteAttachmentCommandHandler from "./commands/upload-attachments/UploadNoteAttachmentCommandHandler";

export const ConversationNoteFeatures: FeatureList[] = [
  {
    command: UploadNoteAttachmentCommand,
    handler: UploadNoteAttachmentCommandHandler,
  },
];
