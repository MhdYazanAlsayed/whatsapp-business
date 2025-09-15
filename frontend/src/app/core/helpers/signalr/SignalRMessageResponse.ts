import ConversationId from "../../entities/conversations/keys/ConversationId";
import { MessageType } from "../../entities/messages/enums/MessageType";
import { SignalRFlowMessageResponse } from "./SignalRFlowMessageResponse";

export default interface SignalRMessageResponse {
  content?: string;
  conversationId: ConversationId;
  received: boolean;
  type: MessageType;
  isNotify: boolean;
  createdAt: string;
  flowMessage?: SignalRFlowMessageResponse;
}
