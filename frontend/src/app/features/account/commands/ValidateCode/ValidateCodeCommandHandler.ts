import { IRequestHandler } from "src/app/core/helpers/mediatR/IRequestHandler";
import ValidateCodeCommand from "./ValidateCodeCommand";
import ServiceProvider from "src/app/core/util/ServiceProvider";
import { IHttp } from "src/app/core/contracts/IHttp";
import ValidateCodeResponse from "./ValidateCodeResponse";
import { IAuthenticator } from "src/app/core/contracts/IAuthenticator";
import ILocalStorage from "src/app/core/contracts/ILocalStorage";
import AuthIdentity from "src/app/core/helpers/AuthIdentity";
export default class ValidateCodeCommandHandler
  implements IRequestHandler<ValidateCodeCommand, ValidateCodeResponse>
{
  private readonly _httpService: IHttp;
  private readonly _authenticator: IAuthenticator;
  private readonly _localStorage: ILocalStorage;

  constructor({
    httpService,
    authenticator,
    localStorageService,
  }: ServiceProvider) {
    this._httpService = httpService;
    this._authenticator = authenticator;
    this._localStorage = localStorageService;
  }

  async HandleAsync(
    request: ValidateCodeCommand
  ): Promise<ValidateCodeResponse> {
    const response = await this._httpService.getData(
      "api/employees/validate?code=" + request.payload.code
    );
    if (!response.ok) {
      console.error(await response.json());
      throw new Error();
    }

    const result = (await response.json()) as ValidateCodeResponse;
    const data = {
      email: result.email!,
      englishName: result.englishName!,
      arabicName: result.arabicName!,
      expirationDate: result.validTo!,
      id: { value: result.id! },
    } as AuthIdentity;

    this._authenticator.identity = data;
    this._localStorage.setItem("authentication", data);

    return result;
  }
}
