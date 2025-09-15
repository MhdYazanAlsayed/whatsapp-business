import GetConvertOptionsResponse from "../api/conversations/get-convert-options/GetConvertOptionsResponse";
import { IHttp } from "../core/contracts/IHttp";

export default class ConvertOptionsService {
  private endpoint = "api/convert-options";

  constructor(readonly httpService: IHttp) {}

  async getAsync(
    conversationId?: string
  ): Promise<GetConvertOptionsResponse[]> {
    let _ = this.endpoint;
    if (conversationId) {
      _ += "conversationId=" + conversationId;
    }

    const response = await this.httpService.getData(_);
    if (!response.ok) {
      throw new Error();
    }

    return (await response.json()) as GetConvertOptionsResponse[];
  }
}
