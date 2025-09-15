import ILocalStorage from "src/app/core/contracts/ILocalStorage";
import { IAuthenticator } from "../../core/contracts/IAuthenticator";
import AuthIdentity from "../../core/helpers/AuthIdentity";

export default class AutheticatorService implements IAuthenticator {
  public isAuthenticated: boolean = false;
  private _identity: AuthIdentity | null = null;

  constructor(readonly _localStorage: ILocalStorage) {}

  // public get accessToken(): string | null {
  //   return this._identity?.accessToken ?? null;
  // }

  public get identity() {
    return this._identity;
  }

  public set identity(data: AuthIdentity | null) {
    this.isAuthenticated = data !== null;
    this._identity = data;
  }

  public loadIdentity() {
    var data = this._localStorage.getItem<AuthIdentity>("authentication");
    if (!data) return;

    if (new Date(data!.expirationDate).getTime() > new Date().getTime()) {
      this.identity = data;
      return;
    }

    this._localStorage.removeItem("authentication");
  }

  public logout() {
    this._localStorage.removeItem("authentication");
    this.identity = null;
  }

  // public isAdmin() {
  //   return this.identity?.userName.toLocaleLowerCase() === "admin";
  // }
}
