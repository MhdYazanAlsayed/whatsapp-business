import { IRequestHandler } from "src/app/core/helpers/mediatR/IRequestHandler";
import UploadTemplateMediaCommand from "./UploadTemplateMediaCommand";
import UploadTemplateMediaCommandResult from "./UploadTemplateMediaCommandResult";
import ServiceProvider from "src/app/core/util/ServiceProvider";
import { IHttp } from "src/app/core/contracts/IHttp";
import { ILoading } from "src/app/core/contracts/ILoading";

export default class UploadTemplateMediaCommandHandler
  implements
    IRequestHandler<
      UploadTemplateMediaCommand,
      UploadTemplateMediaCommandResult
    >
{
  private readonly _httpService: IHttp;
  private readonly _loadingService: ILoading;

  constructor({ httpService, loadingService }: ServiceProvider) {
    this._httpService = httpService;
    this._loadingService = loadingService;
  }

  async HandleAsync(
    request: UploadTemplateMediaCommand
  ): Promise<UploadTemplateMediaCommandResult> {
    this._loadingService.setLoading(true);

    try {
      const formData = new FormData();
      formData.append("file", request.payload.file);

      const response = await this._httpService.postDataWithFile(
        "api/templates/upload",
        formData
      );

      if (!response.ok) {
        const errorData = await response.json();
        console.error("Error Response:", errorData);
        throw new Error(errorData.message || "Failed to upload file");
      }

      return (await response.json()) as UploadTemplateMediaCommandResult;
    } catch (error) {
      console.error("Error uploading template media:", error);

      throw new Error(
        error instanceof Error
          ? error.message
          : "An unknown error occurred during the file upload"
      );
    } finally {
      this._loadingService.setLoading(false);
    }
  }
}
