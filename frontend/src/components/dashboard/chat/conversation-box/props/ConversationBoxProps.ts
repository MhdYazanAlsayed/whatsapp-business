import { ConversationOwner } from "src/app/core/enums/ConversationOwner";

export default interface ConversationBoxProps {
  type: ConversationOwner;
  handleRemoveConversation(id: number): void;
}
