export interface ILogger {
  logInformation(msg: string): void;
  logError(msg: string): void;
  logWarning(msg: string): void;
}
