// GetTemplateDetailsQueryHandler.ts

import { IHttp } from "src/app/core/contracts/IHttp";
import { ILoading } from "src/app/core/contracts/ILoading";
import { IRequestHandler } from "src/app/core/helpers/mediatR/IRequestHandler";
import GetTemplateDetailsQuery from "./GetTemplateDetailsQuery";
import Template from "src/app/core/entities/templates/Template";
import ServiceProvider from "src/app/core/util/ServiceProvider";

export default class GetTemplateDetailsQueryHandler
  implements IRequestHandler<GetTemplateDetailsQuery, Template | null>
{
  private readonly _httpService: IHttp;
  private readonly _loadingService: ILoading;

  constructor({ httpService, loadingService }: ServiceProvider) {
    this._httpService = httpService;
    this._loadingService = loadingService;
  }

  async HandleAsync(
    request: GetTemplateDetailsQuery
  ): Promise<Template | null> {
    const url = `api/templates/${request.payload.templateId.value}`;
    this._loadingService.setLoading(true);

    try {
      const response = await this._httpService.getData(url);

      if (!response.ok) {
        const errorData = await response.json();
        console.error("Error response:", errorData);
        throw new Error(
          "Failed to fetch template details: " + (errorData.message || "")
        );
      }

      return (await response.json()) as Template;
    } catch (error) {
      console.error(
        "An error occurred while fetching template details:",
        error
      );
      throw new Error(
        "An unexpected error occurred while fetching template details."
      );
    } finally {
      this._loadingService.setLoading(false);
    }
  }
}
