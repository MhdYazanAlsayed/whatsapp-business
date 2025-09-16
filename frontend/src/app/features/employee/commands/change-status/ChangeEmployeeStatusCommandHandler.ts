import { IRequestHandler } from "src/app/core/helpers/app_helpers/IRequestHandler";
import ChangeEmployeeStatusCommand from "./ChangeEmployeeStatusCommand";
import { AccountStatusResponse } from "./AccountStatusResponse";
import { IHttp } from "src/app/core/contracts/IHttp";
import { ILoading } from "src/app/core/contracts/ILoading";
import ServiceProvider from "src/app/core/util/ServiceProvider";

export default class ChangeEmployeeStatusCommandHandler
  implements
    IRequestHandler<ChangeEmployeeStatusCommand, AccountStatusResponse>
{
  private readonly _httpService: IHttp;
  private readonly _loadingService: ILoading;

  constructor({ httpService, loadingService }: ServiceProvider) {
    this._httpService = httpService;
    this._loadingService = loadingService;
  }

  async handleAsync(
    request: ChangeEmployeeStatusCommand
  ): Promise<AccountStatusResponse> {
    this._loadingService.setLoading(true);

    try {
      // First, get the current status
      const statusResponse = await this._httpService.getData(
        "api/employees/status"
      );

      if (!statusResponse.ok) {
        throw new Error("Failed to get current status");
      }

      const currentStatus =
        (await statusResponse.json()) as AccountStatusResponse;

      // If the current status is different from the requested status, make the change
      if (currentStatus.isActive !== request.payload.isActive) {
        const endpoint = request.payload.isActive
          ? "api/employees/activate"
          : "api/employees/deactivate";
        const response = await this._httpService.putData(endpoint);

        if (!response.ok) {
          console.error("Error response", await response.json());
          throw new Error("Failed to change employee status");
        }
      }

      // Return the new status
      return {
        isActive: request.payload.isActive,
      };
    } catch (error) {
      console.error("Error changing employee status", error);
      throw error;
    } finally {
      this._loadingService.setLoading(false);
    }
  }
}
