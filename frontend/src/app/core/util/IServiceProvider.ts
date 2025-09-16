import IConversation from "../contracts/conversations/IConversation";
import { IAuthenticator } from "../contracts/IAuthenticator";
import IConvertOption from "../contracts/IConvertOption";
import { IHostEnviroment } from "../contracts/IHostEnviroment";
import { IHttp } from "../contracts/IHttp";
import { ILanguage } from "../contracts/ILanguage";
import { ILoading } from "../contracts/ILoading";
import ILocalStorage from "../contracts/ILocalStorage";
import { ILogger } from "../contracts/ILogger";
import ISignal from "../contracts/ISingalR";
import IUser from "../contracts/IUser";
import IWorkGroup from "../contracts/IWorkGroup";

export default interface IServiceProvider {
  hostEnvironment: IHostEnviroment;
  localStorageService: ILocalStorage;
  languageService: ILanguage;
  authenticator: IAuthenticator;
  loggerService: ILogger;
  loadingService: ILoading;
  httpService: IHttp;
  singalRService: ISignal;
  conversationSerivce: IConversation;
  workGroupService: IWorkGroup;
  applicationUserService: IUser;
  convertOptionService: IConvertOption;
}
