import { IRequestHandler } from "src/app/core/helpers/app_helpers/IRequestHandler";
import GetAccountStatusCommand from "./GetAccountStatusCommand";
import ServiceProvider from "src/app/core/util/ServiceProvider";
import { IHttp } from "src/app/core/contracts/IHttp";
import { SimpleResultDto } from "src/app/core/helpers/TaskResults";

export default class GetAccountStatusCommandHandler
  implements IRequestHandler<GetAccountStatusCommand, SimpleResultDto>
{
  private readonly _httpService: IHttp;
  private readonly endpoint: string = "api/employees";

  constructor({ httpService }: ServiceProvider) {
    this._httpService = httpService;
  }

  async handleAsync({}: GetAccountStatusCommand): Promise<SimpleResultDto> {
    const response = await this._httpService.getData(this.endpoint + "/status");

    if (!response.ok) {
      throw new Error("Failed to get account status");
    }

    const result = (await response.json()) as { status: boolean };

    return { succeeded: result.status } as SimpleResultDto;
  }
}
