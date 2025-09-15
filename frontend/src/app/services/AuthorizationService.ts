import { IAuthorization } from "../core/contracts/IAuthorization";

export default class AuthorizationService implements IAuthorization {
  accessToken: string | null;

  constructor() {
    this.accessToken = null;
  }

  setAccessToken(accessToken: string | null): void {
    this.accessToken = accessToken;
  }
}
