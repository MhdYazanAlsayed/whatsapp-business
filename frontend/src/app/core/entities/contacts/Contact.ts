import Conversation from "../conversations/Conversation";
import ContactId from "./keys/ContactId";

export default interface Contact {
  id: ContactId;
  fullName: string;
  phoneNumber: string;
  nickName?: string;
  conversation: Conversation;
}
