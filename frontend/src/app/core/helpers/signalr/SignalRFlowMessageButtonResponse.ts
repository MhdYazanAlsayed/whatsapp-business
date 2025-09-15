import FlowMessageButtonId from "../../entities/flow-messages/flow-message-buttons/keys/FlowMessageButtonId";
import FlowMessageId from "../../entities/flow-messages/flow-message/keys/FlowMessageId";

export interface SignalRFlowMessageButtonResponse {
  id: FlowMessageButtonId;
  displayText: string;
  next: FlowMessageId;
}
