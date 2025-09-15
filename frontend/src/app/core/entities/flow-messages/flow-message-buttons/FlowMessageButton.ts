import FlowMessage from "../flow-message/FlowMessage";
import FlowMessageId from "../flow-message/keys/FlowMessageId";
import FlowMessageButtonId from "./keys/FlowMessageButtonId";

export default interface FlowMessageButton {
  id: FlowMessageButtonId;
  displayText: string;
  next: FlowMessageId;
  flowMessageId: FlowMessageId;
  flowMessage?: FlowMessage;
}
