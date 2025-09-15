import { IHttp } from "src/app/core/contracts/IHttp";
import { ILoading } from "src/app/core/contracts/ILoading";
import ReplyTemplate from "src/app/core/entities/reply-templates/ReplyTemplate";
import { IRequestHandler } from "src/app/core/helpers/mediatR/IRequestHandler";
import ServiceProvider from "src/app/core/util/ServiceProvider";
import CreateReplyTemplateCommand from "./CreateReplyTemplateCommand";

export default class CreateReplyTemplateCommandHandler
  implements IRequestHandler<CreateReplyTemplateCommand, ReplyTemplate>
{
  private readonly _httpService: IHttp;
  private readonly _loadingService: ILoading;

  constructor({ httpService, loadingService }: ServiceProvider) {
    this._httpService = httpService;
    this._loadingService = loadingService;
  }

  async HandleAsync(
    request: CreateReplyTemplateCommand
  ): Promise<ReplyTemplate> {
    try {
      const url = "api/reply-templates/";
      const payload = {
        title: request.payload.title,
        content: request.payload.content,
      };

      this._loadingService.setLoading(true);
      const response = await this._httpService.postData(url, payload);
      this._loadingService.setLoading(false);

      if (!response.ok) {
        console.error("Error creating reply template:", await response.json());
        throw new Error("Failed to create reply template");
      }

      return (await response.json()) as ReplyTemplate;
    } catch (error) {
      console.error("Error in CreateReplyTemplateCommandHandler:", error);
      throw error;
    }
  }
}
