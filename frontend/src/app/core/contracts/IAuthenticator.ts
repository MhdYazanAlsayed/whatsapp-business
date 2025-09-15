import AuthIdentity from "../helpers/AuthIdentity";

export interface IAuthenticator {
  isAuthenticated: boolean;
  set identity(data: AuthIdentity | null);
  get identity();
  
  loadIdentity(): void;
  logout(): void;
}
