import { toast } from "react-toastify";
import IConversation from "../../core/contracts/conversations/IConversation";
import { IHttp } from "../../core/contracts/IHttp";
import Conversation from "../../core/entities/conversations/Conversation";
import { ConversationOwner } from "../../core/enums/ConversationOwner";
import Message from "../../core/entities/messages/Message";
import PaginationDto from "../../core/helpers/PaginationDto";
import ConversationDetailsResponse from "../../api/conversations/details/ConversationDetailsResponse";
import ConvertConversationRequest from "src/app/api/conversations/convert-conversation/ConvertConversationRequest";
import GetConvertOptionsResponse from "src/app/api/conversations/get-convert-options/GetConvertOptionsResponse";

export default class ConversationService implements IConversation {
  endpoint = "api/conversations";

  constructor(readonly _httpService: IHttp) {}

  async getAsync(page: number, owner?: ConversationOwner) {
    let _ = this.endpoint;
    if (owner != undefined) {
      _ +=
        owner == ConversationOwner.Bot
          ? "/bot"
          : owner == ConversationOwner.CustomerService
          ? "/customer-service"
          : "/";
    }

    _ += "?page=" + page;

    const response = await this._httpService.getData(_);

    if (!response.ok) {
      toast.error("");
      throw new Error();
    }

    return (await response.json()) as PaginationDto<Conversation[]>;
  }

  async takeAsync(id: string): Promise<any> {
    const response = await this._httpService.postData(
      this.endpoint + `/${id}/take`
    );
    if (!response || !response.ok) {
      toast.error("حدث خطأ ما .");
      return { succeeded: false };
    }

    return { succeeded: true };
  }

  async leaveAsync(id: string): Promise<any> {
    const response = await this._httpService.postData(
      this.endpoint + `/${id}/leave`
    );
    if (!response || !response.ok) {
      toast.error("حدث خطأ ما .");
      return { succeeded: false };
    }

    return { succeeded: true };
  }

  async getCustomerServiceAsync(page: number) {
    const response = await this._httpService.getData(
      this.endpoint + "/customer-service?page=" + page
    );

    if (!response.ok) {
      throw new Error();
    }

    return (await response.json()) as PaginationDto<Conversation[]>;
  }

  async getBotAsync() {
    const response = await this._httpService.getData(this.endpoint + "/bot");

    if (!response || response.ok) {
      toast.error("");
      return [];
    }

    return (await response.json()) as Conversation[];
  }

  async fetchMessagesAsync(id: string, page: number) {
    const response = await this._httpService.getData(
      this.endpoint + "/" + id + "/messages?page=" + page
    );
    if (!response || !response.ok) {
      toast.error("حدث خطأ ما");
      return null;
    }

    return (await response.json()) as PaginationDto<Message[]>;
  }

  async detailsAsync(id: string) {
    const response = await this._httpService.getData(this.endpoint + "/" + id);
    console.log(response);
    return (await response.json()) as ConversationDetailsResponse;
  }

  async sendMessageAsync(
    conversationId: string,
    message: string
  ): Promise<any> {
    const response = await this._httpService.postData(
      this.endpoint + "/" + conversationId + "/send-message",
      { content: message }
    );
    if (!response || !response.ok) {
      console.error(response);
      return { succeeded: false };
    }

    return { succeeded: true };
  }

  async convertAsync(
    id: string,
    data: ConvertConversationRequest
  ): Promise<any> {
    const response = await this._httpService.postData(
      this.endpoint + "/" + id + "/convert",
      data
    );

    if (!response || !response.ok) {
      toast.error("حدث خطأ ما .");
      return { succeeded: false };
    }

    return { succeeded: true };
  }

  async getConvertOptionsAsync(id: string) {
    const response = await this._httpService.getData(
      this.endpoint + `/${id}/convert-options`
    );

    if (!response.ok) {
      toast.error("حدثت مشكلة ما .");
      throw new Error("");
    }

    return (await response.json()) as GetConvertOptionsResponse[];
  }

  async getWorkGroupConversationsAsync(id: string, page: number) {
    const response = await this._httpService.getData(
      this.endpoint + "/workgroup/" + id + "?page=" + page
    );

    if (!response.ok) {
      throw new Error();
    }

    return (await response.json()) as PaginationDto<Conversation[]>;
  }
}
