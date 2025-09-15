import Conversation from "../entities/conversations/Conversation";

export default interface ConversationRealtimeChanges {
  add: boolean;
  conversation: Conversation;
}
