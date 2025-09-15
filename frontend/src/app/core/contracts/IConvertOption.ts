import GetConvertOptionsResponse from "src/app/api/conversations/get-convert-options/GetConvertOptionsResponse";

export default interface IConvertOption {
  getAsync(conversationId?: string): Promise<GetConvertOptionsResponse[]>;
}
