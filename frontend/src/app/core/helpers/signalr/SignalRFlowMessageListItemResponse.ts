import FlowMessageListItemId from "../../entities/flow-messages/flow-message-list/keys/FlowMessageListItemId";
import FlowMessageId from "../../entities/flow-messages/flow-message/keys/FlowMessageId";

export interface SignalRFlowMessageListItemResponse {
  id: FlowMessageListItemId;
  content: string;
  description?: string;
  next: FlowMessageId;
}
