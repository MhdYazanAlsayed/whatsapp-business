import Conversation from "../conversations/Conversation";
import ConversationId from "../conversations/keys/ConversationId";
import ConversationHistory from "../conversation-histories/ConversationHistory";
import FlowMessage from "../flow-messages/flow-message/FlowMessage";
import FlowMessageId from "../flow-messages/flow-message/keys/FlowMessageId";
import MessageId from "./keys/MessageId";
import Employee from "../employees/Employee";
import { MessageType } from "./enums/MessageType";
import TemplateMessage from "./templates/TemplateMessage";
import TemplateMessageId from "./templates/keys/TemplateMessageId";

export default interface Message {
  id: MessageId;
  content?: string;
  received: boolean;
  conversationId: ConversationId;
  conversation?: Conversation;
  type: MessageType;
  isNotify: boolean;
  flowMessageId?: FlowMessageId;
  flowMessage?: FlowMessage;
  createdAt: string;
  history?: ConversationHistory;
  createdBy?: string;
  senderId?: string;
  sender?: Employee;
  templateMessage?: TemplateMessage;
  templateMessageId?: TemplateMessageId;
}
