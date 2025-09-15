import IRequest from "src/app/core/helpers/mediatR/IRequest";
import { SaveConversationNoteCommandPayload } from "./SaveConversationNoteCommandPayload";
import ConversationNote from "src/app/core/entities/conversations/conversation-notes/ConversationNote";

export default class SaveConversationNoteCommand extends IRequest<ConversationNote> {
  constructor(public readonly payload: SaveConversationNoteCommandPayload) {
    super();
  }
}
