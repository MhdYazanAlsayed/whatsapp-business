import { IHostEnviroment } from "../core/contracts/IHostEnviroment";
import { ILogger } from "../core/contracts/ILogger";

export default class LoggerService implements ILogger {
  constructor(readonly _hostEnviroment: IHostEnviroment) {}

  logInformation(msg: string) {
    if (!this._hostEnviroment.isDevelopment()) return;

    console.log("%c" + msg, "color: #2196f3;");
  }
  logError(msg: string) {
    if (!this._hostEnviroment.isDevelopment()) return;

    console.log("%c" + msg, "color: #db2a22;");
  }
  logWarning(msg: string) {
    if (!this._hostEnviroment.isDevelopment()) return;

    console.log("%c" + msg, "color: yellow;");
  }
}
