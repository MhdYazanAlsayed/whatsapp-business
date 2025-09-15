// SendTemplateCommandHandler.ts

import { IHttp } from "src/app/core/contracts/IHttp";
import { ILoading } from "src/app/core/contracts/ILoading";
import { IRequestHandler } from "src/app/core/helpers/mediatR/IRequestHandler";
import SendTemplateCommand from "./SendTemplateCommand";
import ServiceProvider from "src/app/core/util/ServiceProvider";
import { ResultDto } from "src/app/core/helpers/ResultDto";
import Message from "src/app/core/entities/messages/Message";

export default class SendTemplateCommandHandler
  implements IRequestHandler<SendTemplateCommand, ResultDto<Message>>
{
  private readonly _httpService: IHttp;
  private readonly _loadingService: ILoading;

  constructor({ httpService, loadingService }: ServiceProvider) {
    this._httpService = httpService;
    this._loadingService = loadingService;
  }

  async HandleAsync(request: SendTemplateCommand): Promise<ResultDto<Message>> {
    const url =
      "api/conversations/" +
      request.payload.conversationId.value +
      "/send-template";
    this._loadingService.setLoading(true);

    try {
      const response = await this._httpService.postData(url, {
        templateId: request.payload.templateId.value,
        components: request.payload.components,
      });

      if (!response.ok) {
        const errorData = await response.json();
        console.error("Error response:", errorData);
        throw new Error(
          "Failed to send template: " + (errorData.message || "")
        );
      }

      return await response.json(); // نجاح العملية
    } catch (error) {
      console.error("An error occurred while sending the template:", error);
      throw new Error(
        "An unexpected error occurred while sending the template."
      );
    } finally {
      this._loadingService.setLoading(false);
    }
  }
}
