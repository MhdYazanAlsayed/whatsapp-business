export default interface UploadNoteAttachmentResult {
  attachments: UploadNoteAttachmentItemResult[];
}
export interface UploadNoteAttachmentItemResult {
  fileName: string;
  fileId: string;
}
