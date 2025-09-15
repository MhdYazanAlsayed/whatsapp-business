import { Enviroment } from "../enums/Enviroment";

export interface IHostEnviroment {
  readonly enviroment: Enviroment;
  isDevelopment(): boolean;
  get apiUrl(): string;
  get frontendUrl(): string;
  get identityProvider(): string;
}
