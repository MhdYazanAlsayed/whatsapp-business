import FeatureList from "src/app/core/helpers/mediatR/FeatureList";
import SendTemplateCommand from "./commands/send-template/SendTemplateCommand";
import SendTemplateCommandHandler from "./commands/send-template/SendTemplateCommandHandler";
import SyncTemplatesCommand from "../templates/commands/sync-temlates/SyncTemplatesCommand";
import SyncTemplatesCommandHandler from "../templates/commands/sync-temlates/SyncTemplatesCommandHandler";
import SaveConversationNoteCommand from "./commands/save-note/SaveConversationNoteCommand";
import SaveConversationNoteCommandHandler from "./commands/save-note/SaveConversationNoteCommandHandler";

export const ConversationFeature: FeatureList[] = [
  {
    command: SendTemplateCommand,
    handler: SendTemplateCommandHandler,
  },
  {
    command: SyncTemplatesCommand,
    handler: SyncTemplatesCommandHandler,
  },
  {
    command: SaveConversationNoteCommand,
    handler: SaveConversationNoteCommandHandler,
  },
];
