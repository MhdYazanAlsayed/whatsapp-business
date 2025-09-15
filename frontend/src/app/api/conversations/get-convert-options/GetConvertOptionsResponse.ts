import { ConversationOwner } from "src/app/core/enums/ConversationOwner";

export default interface GetConvertOptionsResponse {
  id: string;
  type: ConversationOwner;
  text: string;
}
