import { ConversationOwner } from "../../enums/ConversationOwner";
import Contact from "../contacts/Contact";
import ContactId from "../contacts/keys/ContactId";
import Message from "../messages/Message";
import Employee from "../employees/Employee";
import ConversationNote from "./conversation-notes/ConversationNote";
import ConversationId from "./keys/ConversationId";

export default interface Conversation {
  id: ConversationId;
  contact?: Contact;
  contactId: ContactId;
  owner: ConversationOwner;
  customerServiceUserId?: string;
  customerServiceUser?: Employee;
  fromBot: boolean;
  messages?: Message[];
  createdAt: string;
  updatedAt?: string;
  note?: ConversationNote;
}
