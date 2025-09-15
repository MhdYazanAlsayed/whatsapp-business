import FlowMessage from "../flow-message/FlowMessage";
import FlowMessageId from "../flow-message/keys/FlowMessageId";
import FlowMessageListItemId from "./keys/FlowMessageListItemId";

export default interface FlowMessageListItem {
  id: FlowMessageListItemId;
  content: string;
  description?: string;
  next: FlowMessageId;
  flowMessageId: FlowMessageId;
  flowMessage?: FlowMessage;
}
