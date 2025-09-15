import ConversationId from "src/app/core/entities/conversations/keys/ConversationId";
import { SaveConversationNoteAttachmentToAddDto } from "./dtos/SaveConversationNoteAttachmentToAddDto";
import { SaveConversationNoteAttachmentToDeleteDto } from "./dtos/SaveConversationNoteAttachmentToDeleteDto";

export interface SaveConversationNoteCommandPayload {
  id: ConversationId;
  content: string;
  attachmentsToDelete: SaveConversationNoteAttachmentToDeleteDto[];
  attachmentsToAdd: SaveConversationNoteAttachmentToAddDto[];
}
