// import LoginRequest from "../../api/user-service/Login/LoginRequest";
// import AuthResponse from "../../api/user-service/Login/AuthResponse";
// import { IUserService } from "../../core/contracts/IUserService";
// import { ResultDto } from "../../core/helpers/ResultDto";
// import WorkGroup from "src/app/core/entities/work-groups/WorkGroup";
// import GetAccountStatusResponse from "src/app/api/user-service/GetAccountStatus/GetAccountStatusResponse";

// export default class UserService implements IUserService {
//   loginAsync(data: LoginRequest): Promise<ResultDto<AuthResponse>> {
//     throw new Error("Method not implemented.");
//   }
//   isAuthenticated(): boolean {
//     throw new Error("Method not implemented.");
//   }
//   logout(): void {
//     throw new Error("Method not implemented.");
//   }
//   get accessToken(): string | null {
//     throw new Error("Method not implemented.");
//   }
//   getWorkGroupsAsync(): Promise<WorkGroup[]> {
//     throw new Error("Method not implemented.");
//   }
//   getAccountStatusAsync(): Promise<GetAccountStatusResponse> {
//     throw new Error("Method not implemented.");
//   }
//   activateAsync(): Promise<any> {
//     throw new Error("Method not implemented.");
//   }
//   deactiveAsync(): Promise<any> {
//     throw new Error("Method not implemented.");
//   }
//   // private readonly endpoint = "api/employees";

//   // constructor(
//   //   readonly _httpService: IHttp,
//   //   readonly _authonticator: IAuthenticator,
//   //   readonly _localStorage: ILocalStorage,
//   //   readonly _languageService: ILanguage
//   // ) {}

//   // get accessToken(): string | null {
//   //   // return this._authonticator.accessToken;
//   //   return "";
//   // }

//   // public logout() {
//   //   this._authonticator.identity = null;
//   //   this._localStorage.removeItem("authentication");
//   // }

//   // public isAuthenticated(): boolean {
//   //   return this._authonticator.isAuthenticated;
//   // }

//   // // public loadIdentity(): boolean {
//   // //   var data = this._localStorage.getItem<AuthIdentity>("authentication");
//   // //   if (!data) return false;

//   // //   // Check the expiration date overhere ...

//   // //   this._authonticator.identity = data;
//   // //   return true;
//   // // }

//   // async loginAsync({
//   //   userName,
//   //   password,
//   //   rememberMe,
//   // }: LoginRequest): Promise<ResultDto<AuthResponse>> {
//   //   const response = await this._httpService.postData(
//   //     this.endpoint + "/login",
//   //     {
//   //       userName,
//   //       password,
//   //     }
//   //   );

//   //   if (!response.ok) {
//   //     toast.error("لم تنجح عملية تسجيل الدخول");

//   //     return { succeeded: false };
//   //   }

//   //   let responseAsJson = (await response.json()) as ResultDto<AuthResponse>;

//   //   let data = {
//   //     accessToken: responseAsJson.entity?.accessToken!,
//   //     refreshToken: "",
//   //     userName: responseAsJson.entity?.userName,
//   //     expirationDate: "",
//   //     userId: responseAsJson.entity?.userId,
//   //     email: responseAsJson.entity?.email,
//   //     fullName: responseAsJson.entity?.fullName,
//   //   } as AuthIdentity;

//   //   if (rememberMe) {
//   //     this._localStorage.setItem("authentication", data);
//   //   }

//   //   this._authonticator.identity = data;

//   //   return responseAsJson;
//   // }

//   // async getWorkGroupsAsync() {
//   //   const response = await this._httpService.getData(
//   //     this.endpoint + "/workgroups"
//   //   );

//   //   if (!response.ok) {
//   //     throw new Error();
//   //   }

//   //   return (await response.json()) as WorkGroup[];
//   // }

//   // async getAccountStatusAsync() {
//   //   const response = await this._httpService.getData(this.endpoint + "/status");

//   //   return (await response.json()) as GetAccountStatusResponse;
//   // }

//   // async activateAsync(): Promise<TaskResult> {
//   //   const response = await this._httpService.putData(
//   //     this.endpoint + "/activate"
//   //   );

//   //   if (!response.ok) {
//   //     return { succeeded: false };
//   //   }

//   //   return { succeeded: true };
//   // }

//   // async deactiveAsync(): Promise<TaskResult> {
//   //   const response = await this._httpService.putData(
//   //     this.endpoint + "/deactivate"
//   //   );

//   //   if (!response.ok) {
//   //     return { succeeded: false };
//   //   }

//   //   return { succeeded: true };
//   // }
// }
