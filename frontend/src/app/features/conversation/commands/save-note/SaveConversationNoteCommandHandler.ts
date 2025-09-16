import { IRequestHandler } from "src/app/core/helpers/app_helpers/IRequestHandler";
import SaveConversationNoteCommand from "./SaveConversationNoteCommand";
import { IHttp } from "src/app/core/contracts/IHttp";
import { ILoading } from "src/app/core/contracts/ILoading";
import ServiceProvider from "src/app/core/util/ServiceProvider";
import ConversationNote from "src/app/core/entities/conversations/conversation-notes/ConversationNote";

export default class SaveConversationNoteCommandHandler
  implements IRequestHandler<SaveConversationNoteCommand, ConversationNote>
{
  private readonly _httpService: IHttp;
  private readonly _loadingService: ILoading;

  constructor({ httpService, loadingService }: ServiceProvider) {
    this._httpService = httpService;
    this._loadingService = loadingService;
  }

  async handleAsync(
    request: SaveConversationNoteCommand
  ): Promise<ConversationNote> {
    this._loadingService.setLoading(true);

    try {
      const response = await this._httpService.postData(
        "api/conversations/" + request.payload.id.value + "/note",
        {
          content: request.payload.content,
          attachmentsToAdd: request.payload.attachmentsToAdd,
          attachmentsToDelete: request.payload.attachmentsToDelete,
        }
      );

      if (!response.ok) {
        console.error("Error response", await response.json());
        throw new Error("Failed to save conversation");
      }

      return await response.json();
    } catch (error) {
      console.error("Error saving conversation", error);
      throw error;
    } finally {
      this._loadingService.setLoading(false);
    }
  }
}
