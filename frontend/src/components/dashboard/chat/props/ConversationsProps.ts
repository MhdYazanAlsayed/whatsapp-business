import Conversation from "src/app/core/entities/conversations/Conversation";
import { ConversationOwner } from "src/app/core/enums/ConversationOwner";

export default interface ConversationProps {
  type: ConversationOwner;
  conversations: Conversation[];
  setConversations: (value: Conversation[]) => void;
}
