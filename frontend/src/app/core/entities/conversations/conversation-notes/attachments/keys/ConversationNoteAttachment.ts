import Entity from "src/app/core/entities/base/Entity";
import ConversationNoteAttachmentId from "./ConversationNoteAttachmentId";
import ConversationNote from "../../ConversationNote";
import ConversationNoteId from "../../keys/ConversationNoteId";

export default interface ConversationNoteAttachment extends Entity {
  id: ConversationNoteAttachmentId;
  fileName: string;
  fileId: string;
  conversationNote?: ConversationNote;
  conversationNoteId: ConversationNoteId;
}
