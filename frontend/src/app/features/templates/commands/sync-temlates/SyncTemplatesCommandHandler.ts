// SyncTemplatesCommandHandler.ts

import { IHttp } from "src/app/core/contracts/IHttp";
import { ILoading } from "src/app/core/contracts/ILoading";
import { IRequestHandler } from "src/app/core/helpers/mediatR/IRequestHandler";
import SyncTemplatesCommand from "./SyncTemplatesCommand";
import ServiceProvider from "src/app/core/util/ServiceProvider";
import { SimpleResultDto } from "src/app/core/helpers/TaskResults";

export default class SyncTemplatesCommandHandler
  implements IRequestHandler<SyncTemplatesCommand, SimpleResultDto>
{
  private readonly _httpService: IHttp;
  private readonly _loadingService: ILoading;

  constructor({ httpService, loadingService }: ServiceProvider) {
    this._httpService = httpService;
    this._loadingService = loadingService;
  }

  async HandleAsync(_: SyncTemplatesCommand): Promise<SimpleResultDto> {
    const url = "api/templates/sync";
    this._loadingService.setLoading(true);

    try {
      const response = await this._httpService.postData(url, {});

      if (!response.ok) {
        const errorData = await response.json();
        console.error("Error response:", errorData);
        return new SimpleResultDto(false);
      }

      return new SimpleResultDto(true);
    } catch (error) {
      console.error("An error occurred while syncing templates:", error);
      throw new Error("An unexpected error occurred while syncing templates.");
    } finally {
      this._loadingService.setLoading(false);
    }
  }
}
