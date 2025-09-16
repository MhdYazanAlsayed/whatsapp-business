import { IHttp } from "src/app/core/contracts/IHttp";
import { ILoading } from "src/app/core/contracts/ILoading";
import ReplyTemplate from "src/app/core/entities/reply-templates/ReplyTemplate";
import { IRequestHandler } from "src/app/core/helpers/app_helpers/IRequestHandler";
import PaginationDto from "src/app/core/helpers/PaginationDto";
import ServiceProvider from "src/app/core/util/ServiceProvider";
import GetReplyTemplatePaginationQuery from "./GetReplyTemplatePaginationQuery";

export default class GetReplyTemplatePaginationQueryHandler
  implements
    IRequestHandler<
      GetReplyTemplatePaginationQuery,
      PaginationDto<ReplyTemplate[]>
    >
{
  private readonly _httpService: IHttp;
  private readonly _loadingService: ILoading;

  constructor({ httpService, loadingService }: ServiceProvider) {
    this._httpService = httpService;
    this._loadingService = loadingService;
  }

  async handleAsync(
    request: GetReplyTemplatePaginationQuery
  ): Promise<PaginationDto<ReplyTemplate[]>> {
    try {
      const url = `api/reply-templates/pagination?page=${request.payload.page}`;

      this._loadingService.setLoading(true);
      const response = await this._httpService.getData(url);
      this._loadingService.setLoading(false);

      if (!response.ok) {
        console.error("Error fetching reply templates:", await response.json());
        throw new Error("Failed to fetch reply templates");
      }

      return (await response.json()) as PaginationDto<ReplyTemplate[]>;
    } catch (error) {
      console.error("Error in GetReplyTemplatePaginationQueryHandler:", error);
      throw error;
    }
  }
}
