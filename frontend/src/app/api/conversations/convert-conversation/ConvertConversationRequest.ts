import { ConversationOwner } from "src/app/core/enums/ConversationOwner";

export default interface ConvertConversationRequest {
  to: ConversationOwner;
  userId?: string;
  workGroupId?: string;
}
