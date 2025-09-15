import Conversation from "src/app/core/entities/conversations/Conversation";
import SelectOption from "src/app/core/helpers/SelectOption";

export default interface ConversationProps {
  data: Conversation;
  members: SelectOption[];
  handleRemoveConversation(id: number): void;
}
