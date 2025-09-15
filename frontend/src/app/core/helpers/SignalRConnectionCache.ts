import { HubConnection } from "@microsoft/signalr";

export default interface SignalRConnectionCache {
  key: string;
  connection: HubConnection;
}
