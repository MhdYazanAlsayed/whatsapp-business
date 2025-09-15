import { ConversationOwner } from "src/app/core/enums/ConversationOwner";
import SelectOption from "src/app/core/helpers/SelectOption";

export default interface ConvertSelectOption extends SelectOption {
  type: ConversationOwner;
}
