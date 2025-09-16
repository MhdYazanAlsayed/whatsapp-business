import WorkGroupService from "src/app/services/WorkGroupService";
import ConversationService from "../../services/Conversations/ConversationService";
import HostEnviroment from "../../services/HostEnviroment";
import HttpService from "../../services/HttpService";
import LanguageService from "../../services/LanguageService";
import LoadingService from "../../services/LoadingService";
import LocalStorageService from "../../services/LocalStorageService";
import LoggerService from "../../services/LoggerService";
import SignalRService from "../../services/SignalRService";
import AutheticatorService from "../../services/UserServices/AuthenticatorService";
import IConversation from "../contracts/conversations/IConversation";
import { IAuthenticator } from "../contracts/IAuthenticator";
import { IHostEnviroment } from "../contracts/IHostEnviroment";
import { IHttp } from "../contracts/IHttp";
import { ILanguage } from "../contracts/ILanguage";
import { ILoading } from "../contracts/ILoading";
import ILocalStorage from "../contracts/ILocalStorage";
import { ILogger } from "../contracts/ILogger";
import ISignal from "../contracts/ISingalR";
import IWorkGroup from "../contracts/IWorkGroup";
import IUser from "../contracts/IUser";
import ApplicationUserService from "src/app/services/ApplicationUserService";
import IConvertOption from "../contracts/IConvertOption";
import ConvertOptionsService from "src/app/services/ConvertOptionsService";
import IServiceProvider from "./IServiceProvider";

export default class ServiceProvider implements IServiceProvider {
  public hostEnvironment: IHostEnviroment = new HostEnviroment();
  public localStorageService: ILocalStorage = new LocalStorageService();
  public languageService: ILanguage = new LanguageService(
    this.localStorageService
  );
  public authenticator: IAuthenticator = new AutheticatorService(
    this.localStorageService
  );
  public loggerService: ILogger = new LoggerService(this.hostEnvironment);
  public loadingService: ILoading = new LoadingService();
  public httpService: IHttp = new HttpService(
    this.loggerService,
    this.languageService,
    this.hostEnvironment
  );

  public singalRService: ISignal = new SignalRService(
    this.hostEnvironment,
    this.authenticator
  );
  public conversationSerivce: IConversation = new ConversationService(
    this.httpService
  );
  public workGroupService: IWorkGroup = new WorkGroupService(this.httpService);
  public applicationUserService: IUser = new ApplicationUserService(
    this.httpService
  );
  public convertOptionService: IConvertOption = new ConvertOptionsService(
    this.httpService
  );
}
