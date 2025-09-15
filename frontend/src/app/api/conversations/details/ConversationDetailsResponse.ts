import Conversation from "../../../core/entities/conversations/Conversation";
import Message from "../../../core/entities/messages/Message";
export default interface ConversationDetailsResponse {
  conversation: Conversation;
  messagePages: number;
  messages: Message[];
}
