import { IHttp } from "src/app/core/contracts/IHttp";
import { ILoading } from "src/app/core/contracts/ILoading";
import Template from "src/app/core/entities/templates/Template";
import { IRequestHandler } from "src/app/core/helpers/mediatR/IRequestHandler";
import ServiceProvider from "src/app/core/util/ServiceProvider";
import CreateTemplateCommand from "./CreateTemplateCommand";
import { ResultDto } from "src/app/core/helpers/TaskResults";

export default class CreateTemplateHandler
  implements IRequestHandler<CreateTemplateCommand, ResultDto<Template>>
{
  private readonly _httpService: IHttp;
  private readonly _loadingService: ILoading;

  constructor({ httpService, loadingService }: ServiceProvider) {
    this._httpService = httpService;
    this._loadingService = loadingService;
  }

  async HandleAsync(
    request: CreateTemplateCommand
  ): Promise<ResultDto<Template>> {
    try {
      const url = "api/templates";

      this._loadingService.setLoading(true);
      const response = await this._httpService.postData(url, request.payload);
      this._loadingService.setLoading(false);

      if (!response.ok) {
        console.error("Error creating template:", await response.json());
        throw new Error("Failed to create template");
      }

      return ResultDto.success(await response.json());
    } catch (error) {
      console.error("Error in CreateTemplateHandler:", error);
      return ResultDto.failure();
    }
  }
}
