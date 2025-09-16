import { IRequestHandler } from "src/app/core/helpers/app_helpers/IRequestHandler";
import GetWorkGroupsQuery from "./GetWorkGroupsQuery";
import { IHttp } from "src/app/core/contracts/IHttp";
import { ILoading } from "src/app/core/contracts/ILoading";
import ServiceProvider from "src/app/core/util/ServiceProvider";
import WorkGroup from "src/app/core/entities/work-groups/WorkGroup";

export default class GetWorkGroupsHandler
  implements IRequestHandler<GetWorkGroupsQuery, WorkGroup[]>
{
  private readonly _httpService: IHttp;
  private readonly _loadingService: ILoading;

  constructor({ httpService, loadingService }: ServiceProvider) {
    this._httpService = httpService;
    this._loadingService = loadingService;
  }

  async handleAsync(request: GetWorkGroupsQuery): Promise<WorkGroup[]> {
    this._loadingService.setLoading(true);

    try {
      const queryParams = request.keyword
        ? `?keyword=${encodeURIComponent(request.keyword)}`
        : "";
      const response = await this._httpService.getData(
        `api/work-groups${queryParams}`
      );

      if (!response.ok) {
        console.error("Error response", await response.json());
        throw new Error("Failed to get work groups");
      }

      return await response.json();
    } catch (error) {
      console.error("Error getting work groups", error);
      throw error;
    } finally {
      this._loadingService.setLoading(false);
    }
  }
}
