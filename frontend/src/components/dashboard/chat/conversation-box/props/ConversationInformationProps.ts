import { ConversationOwner } from "src/app/core/enums/ConversationOwner";

export default interface ConversationInformationProps {
  id: string;
  fullName: string;
  owner: ConversationOwner;
  phoneNumber: string;
  handleRemoveConversation(id: number): void;
}
