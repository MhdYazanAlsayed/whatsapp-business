import Entity from "../../base/Entity";
import Conversation from "../Conversation";
import ConversationId from "../keys/ConversationId";
import ConversationNoteAttachment from "./attachments/keys/ConversationNoteAttachment";
import ConversationNoteId from "./keys/ConversationNoteId";

export default interface ConversationNote extends Entity {
  id: ConversationNoteId;
  content: string;
  conversationId: ConversationId;
  conversation?: Conversation;
  attachments: ConversationNoteAttachment[];
}
