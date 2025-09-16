import { IHttp } from "src/app/core/contracts/IHttp";
import { ILoading } from "src/app/core/contracts/ILoading";
import ReplyTemplate from "src/app/core/entities/reply-templates/ReplyTemplate";
import { IRequestHandler } from "src/app/core/helpers/app_helpers/IRequestHandler";
import ServiceProvider from "src/app/core/util/ServiceProvider";
import GetReplyTemplateDetailsQuery from "./GetReplyTemplateDetailsQuery";

export default class GetReplyTemplateDetailsQueryHandler
  implements
    IRequestHandler<GetReplyTemplateDetailsQuery, ReplyTemplate | null>
{
  private readonly _httpService: IHttp;
  private readonly _loadingService: ILoading;

  constructor({ httpService, loadingService }: ServiceProvider) {
    this._httpService = httpService;
    this._loadingService = loadingService;
  }

  async handleAsync(
    request: GetReplyTemplateDetailsQuery
  ): Promise<ReplyTemplate | null> {
    try {
      const url = `api/reply-templates/${request.payload.id.value}`;

      this._loadingService.setLoading(true);
      const response = await this._httpService.getData(url);
      this._loadingService.setLoading(false);

      if (!response.ok) {
        console.error(
          "Error fetching reply template details:",
          await response.json()
        );
        throw new Error("Failed to fetch reply template details");
      }

      return (await response.json()) as ReplyTemplate | null;
    } catch (error) {
      console.error("Error in GetReplyTemplateDetailsQueryHandler:", error);
      throw error;
    }
  }
}
