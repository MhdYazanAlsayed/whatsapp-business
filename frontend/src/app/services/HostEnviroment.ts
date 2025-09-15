import { IHostEnviroment } from "../core/contracts/IHostEnviroment";
import { Enviroment } from "../core/enums/Enviroment";
import Settings from "../core/appsettings.json";

export default class HostEnviroment implements IHostEnviroment {
  // Change this to production when deploying
  enviroment: Enviroment = Enviroment.Production;

  get apiUrl(): string {
    return this.isDevelopment()
      ? Settings.Api.Development
      : Settings.Api.Production;
  }

  get frontendUrl(): string {
    return this.isDevelopment()
      ? Settings.Frontend.Development
      : Settings.Frontend.Production;
  }

  get identityProvider(): string {
    return this.isDevelopment()
      ? Settings.IdentityProvider.Development
      : Settings.IdentityProvider.Production;
  }

  isDevelopment(): boolean {
    return this.enviroment === Enviroment.Development;
  }
}
