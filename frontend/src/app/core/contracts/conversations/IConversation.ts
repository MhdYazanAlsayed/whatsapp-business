import ConvertConversationRequest from "src/app/api/conversations/convert-conversation/ConvertConversationRequest";
import ConversationDetailsResponse from "../../../api/conversations/details/ConversationDetailsResponse";
import Conversation from "../../entities/conversations/Conversation";
import Message from "../../entities/messages/Message";
import { ConversationOwner } from "../../enums/ConversationOwner";
import PaginationDto from "../../helpers/PaginationDto";
import GetConvertOptionsResponse from "src/app/api/conversations/get-convert-options/GetConvertOptionsResponse";

export default interface IConversation {
  getAsync(
    page: number,
    owner?: ConversationOwner
  ): Promise<PaginationDto<Conversation[]>>;
  getCustomerServiceAsync(page: number): Promise<PaginationDto<Conversation[]>>;
  getBotAsync(): Promise<Conversation[]>;
  fetchMessagesAsync(
    id: string,
    page: number
  ): Promise<PaginationDto<Message[]> | null>;
  detailsAsync(id: string): Promise<ConversationDetailsResponse>;
  takeAsync(id: string): Promise<any>;
  leaveAsync(id: string): Promise<any>;
  sendMessageAsync(conversationId: string, message: string): Promise<any>;
  convertAsync(id: string, data: ConvertConversationRequest): Promise<any>;
  getConvertOptionsAsync(id: string): Promise<GetConvertOptionsResponse[]>;
  getWorkGroupConversationsAsync(
    id: string,
    page: number
  ): Promise<PaginationDto<Conversation[]>>;
}
