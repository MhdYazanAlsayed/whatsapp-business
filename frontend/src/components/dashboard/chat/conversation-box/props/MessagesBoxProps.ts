import Conversation from "src/app/core/entities/conversations/Conversation";
import Message from "src/app/core/entities/messages/Message";

export default interface MessagesBoxProps {
  conversation: Conversation;
  handleAddMoreMessages(data: Message[]): void;
  handleRemoveConversation(id: number): void;
  pages: number;
  signalRConnectionKey: string;
}
