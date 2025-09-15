import { ConversationOwner } from "src/app/core/enums/ConversationOwner";
import Index from "../Index";

const UserMessenger = () => {
  return <Index type={ConversationOwner.User} />;
};

export default UserMessenger;
