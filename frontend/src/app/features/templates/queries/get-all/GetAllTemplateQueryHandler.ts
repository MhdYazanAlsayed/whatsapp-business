// GetAllTemplateQueryHandler.ts
import { IHttp } from "src/app/core/contracts/IHttp";
import { ILoading } from "src/app/core/contracts/ILoading";
import Template from "src/app/core/entities/templates/Template";
import { IRequestHandler } from "src/app/core/helpers/app_helpers/IRequestHandler";
import ServiceProvider from "src/app/core/util/ServiceProvider";
import GetAllTemplateQuery from "./GetAllTemplatesQuery";

export default class GetAllTemplateQueryHandler
  implements IRequestHandler<GetAllTemplateQuery, Template[]>
{
  private readonly _httpService: IHttp;
  private readonly _loadingService: ILoading;

  constructor({ httpService, loadingService }: ServiceProvider) {
    this._httpService = httpService;
    this._loadingService = loadingService;
  }

  async handleAsync(_: GetAllTemplateQuery): Promise<Template[]> {
    const url = "api/templates";
    this._loadingService.setLoading(true);

    try {
      const response = await this._httpService.getData(url);

      if (!response.ok) {
        const errorData = await response.json();
        console.error("Error response:", errorData);
        throw new Error(
          "Failed to retrieve templates: " + (errorData.message || "")
        );
      }

      return (await response.json()) as Template[];
    } catch (error) {
      console.error("An error occurred while fetching the templates:", error);
      throw new Error(
        "An unexpected error occurred while fetching the templates."
      );
    } finally {
      this._loadingService.setLoading(false);
    }
  }
}
