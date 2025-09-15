import { ConversationOwner } from "../../enums/ConversationOwner";
import Conversation from "../conversations/Conversation";
import ConversationId from "../conversations/keys/ConversationId";
import Employee from "../employees/Employee";
import WorkGroupId from "../work-groups/keys/WorkGroupId";
import WorkGroup from "../work-groups/WorkGroup";

export default interface ConversationHistory {
  currentOwner: ConversationOwner;
  employeeId?: number;
  employee?: Employee;
  conversationId?: ConversationId;
  conversation?: Conversation;
  workGroupId?: WorkGroupId;
  workGroup?: WorkGroup;
  createdAt: string;
}
