import { RealtimeEvents } from "../helpers/RealtimeNotifyType";

export default interface ISignal {
  build(key: string, endpoint: string): void;
  listen(key: string, event: RealtimeEvents, func: any): void;
  off(key: string, event: RealtimeEvents): void;
  connectionAsync(key: string): Promise<void>;
  disconnectionAsync(key: string): Promise<void>;
}
