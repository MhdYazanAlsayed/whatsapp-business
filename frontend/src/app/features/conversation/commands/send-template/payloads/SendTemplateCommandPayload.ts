// SendTemplateCommandPayload.ts

import ConversationId from "src/app/core/entities/conversations/keys/ConversationId";
import TemplateId from "src/app/core/entities/templates/keys/TemplateId";
import { SendTemplateComponentCommandPayload } from "./SendTemplateComponentCommandPayload";

export interface SendTemplateCommandPayload {
  conversationId: ConversationId;
  templateId: TemplateId;
  components: SendTemplateComponentCommandPayload[];
}
