import { ConversationOwner } from "src/app/core/enums/ConversationOwner";
import Index from "../Index";

const UserMessenger = () => {
  return <Index type={ConversationOwner.Bot} />;
};

export default UserMessenger;
