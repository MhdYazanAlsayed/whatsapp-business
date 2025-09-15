import ConversationId from "../entities/conversations/keys/ConversationId";
import Message from "../entities/messages/Message";

export default interface NewMessageDto {
  conversationId: ConversationId;
  message: Message;
}
