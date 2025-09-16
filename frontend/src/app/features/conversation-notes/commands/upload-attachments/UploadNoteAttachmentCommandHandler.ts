import { IHttp } from "src/app/core/contracts/IHttp";
import { ILoading } from "src/app/core/contracts/ILoading";
import { IRequestHandler } from "src/app/core/helpers/app_helpers/IRequestHandler";
import ServiceProvider from "src/app/core/util/ServiceProvider";
import UploadNoteAttachmentCommand from "./UploadNoteAttachmentCommand";
import UploadNoteAttachmentResult from "./UploadNoteAttachmentResult";

export default class UploadNoteAttachmentCommandHandler
  implements
    IRequestHandler<UploadNoteAttachmentCommand, UploadNoteAttachmentResult>
{
  private readonly _httpService: IHttp;
  private readonly _loadingService: ILoading;

  constructor({ httpService, loadingService }: ServiceProvider) {
    this._httpService = httpService;
    this._loadingService = loadingService;
  }

  async handleAsync(
    request: UploadNoteAttachmentCommand
  ): Promise<UploadNoteAttachmentResult> {
    this._loadingService.setLoading(true);

    try {
      const formData = new FormData();

      Array.from(request.payload.files).forEach((file) => {
        formData.append("files", file);
      });

      const response = await this._httpService.postDataWithFile(
        "api/conversations/notes/attachments/upload",
        formData
      );

      if (!response.ok) {
        const errorData = await response.json();
        console.error("Error Response:", errorData);
        throw new Error(errorData.message || "Failed to upload attachments");
      }

      return response.json();
    } catch (error) {
      console.error("Error uploading note attachments:", error);
      throw new Error(
        error instanceof Error
          ? error.message
          : "An unknown error occurred during the attachment upload"
      );
    } finally {
      this._loadingService.setLoading(false);
    }
  }
}
