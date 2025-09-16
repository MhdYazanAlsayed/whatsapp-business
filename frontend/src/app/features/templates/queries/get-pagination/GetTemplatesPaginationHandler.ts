import { IHttp } from "src/app/core/contracts/IHttp";
import { ILoading } from "src/app/core/contracts/ILoading";
import Template from "src/app/core/entities/templates/Template";
import PaginationDto from "src/app/core/helpers/PaginationDto";
import { IRequestHandler } from "src/app/core/helpers/app_helpers/IRequestHandler";
import ServiceProvider from "src/app/core/util/ServiceProvider";
import GetTemplatesPaginationQuery from "./GetTemplatesPaginationQuery";

export default class GetTemplatesPaginationHandler
  implements
    IRequestHandler<GetTemplatesPaginationQuery, PaginationDto<Template[]>>
{
  private readonly _httpService: IHttp;
  private readonly _loadingService: ILoading;

  constructor({ httpService, loadingService }: ServiceProvider) {
    this._httpService = httpService;
    this._loadingService = loadingService;
  }

  async handleAsync(
    request: GetTemplatesPaginationQuery
  ): Promise<PaginationDto<Template[]>> {
    try {
      const url = `api/templates/pagination?page=${request.payload.page}`;

      this._loadingService.setLoading(true);
      const response = await this._httpService.getData(url);
      this._loadingService.setLoading(false);

      if (!response.ok) {
        console.error(
          "Error fetching templates pagination:",
          await response.json()
        );
        throw new Error("Failed to fetch templates pagination");
      }

      return (await response.json()) as PaginationDto<Template[]>;
    } catch (error) {
      console.error("Error in GetTemplatesPaginationHandler:", error);
      throw error;
    }
  }
}
