import Conversation from "../conversations/Conversation";
import Employee from "../employees/Employee";
import WorkGroupId from "./keys/WorkGroupId";

export default interface WorkGroup {
  id: WorkGroupId;
  name: string;
  usersCount: number;
  users: Employee[];
  conversations: Conversation[];
}
