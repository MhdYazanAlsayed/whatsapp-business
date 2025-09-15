import ConversationNote from "src/app/core/entities/conversations/conversation-notes/ConversationNote";
import ConversationId from "src/app/core/entities/conversations/keys/ConversationId";
import TadawiModalProps from "src/app/core/helpers/TadawiModalProps";

export default interface Props extends TadawiModalProps {
  note?: ConversationNote;
  id: ConversationId;
  handleUpdateConversationNote(note: ConversationNote): void;
}
