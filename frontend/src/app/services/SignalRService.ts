import {
  HubConnectionBuilder,
  HubConnectionState,
  LogLevel,
} from "@microsoft/signalr";
import ISignal from "../core/contracts/ISingalR";
import { IHostEnviroment } from "../core/contracts/IHostEnviroment";
import { IAuthenticator } from "../core/contracts/IAuthenticator";
import SignalRConnectionCache from "../core/helpers/SignalRConnectionCache";
import { RealtimeEvents } from "../core/helpers/RealtimeNotifyType";

export default class SignalRService implements ISignal {
  private _connections: SignalRConnectionCache[] = [];

  constructor(
    readonly _hostEnvironment: IHostEnviroment,
    readonly _authonticator: IAuthenticator
  ) {}

  build(key: string, endpoint: string) {
    if (this._connections.findIndex((x) => x.key == key) !== -1) return;

    var _ = new HubConnectionBuilder()
      .configureLogging(LogLevel.Information)
      .withUrl(this._hostEnvironment.apiUrl + endpoint, {
        // accessTokenFactory: () => this._authonticator.accessToken as string,
      })
      .build();

    this._connections.push({ key: key, connection: _ });
  }

  listen(key: string, event: RealtimeEvents, func: any) {
    const index = this._connections.findIndex((x) => x.key == key);
    if (index == -1) throw new Error();

    this._connections[index].connection?.on(event, func);
  }

  off(key: string, event: RealtimeEvents) {
    const index = this._connections.findIndex((x) => x.key == key);
    if (index == -1) throw new Error();

    this._connections[index].connection?.off(event);
  }

  async connectionAsync(key: string) {
    const index = this._connections.findIndex((x) => x.key == key);
    if (index == -1) throw new Error();

    if (
      this._connections[index].connection?.state == HubConnectionState.Connected
    )
      return;

    await this._connections[index].connection?.start();
  }

  async disconnectionAsync(key: string) {
    const index = this._connections.findIndex((x) => x.key == key);
    if (index == -1) throw new Error();

    if (
      this._connections[index].connection?.state ==
      HubConnectionState.Disconnected
    )
      return;

    await this._connections[index].connection?.stop();
  }

  demolish(key: string) {
    const index = this._connections.findIndex((x) => x.key == key);
    if (index == -1) throw new Error();

    this._connections.splice(index, 1);
  }
}
