export interface IAuthorization {
  accessToken: string | null;
  setAccessToken(accessToken: string | null): void;
}
