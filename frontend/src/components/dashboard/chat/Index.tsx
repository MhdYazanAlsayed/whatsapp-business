import Covnersations from "./conversations/Covnersations";
import ConversationBox from "./conversation-box/ConversationBox";
import ChatProps from "./props/ChatProps";
import { useState } from "react";
import Conversation from "src/app/core/entities/conversations/Conversation";

const Index = ({ type }: ChatProps) => {
  const [conversations, setConversations] = useState<Conversation[]>([]);

  const handleRemoveConversation = (id: number) => {
    setConversations((prev) => {
      const index = prev.findIndex((x) => x.id.value == id);
      if (index === -1) throw new Error();

      prev.splice(index, 1);
      return [...prev];
    });
  };

  return (
    <section className="chat">
      <div className="row h-100p">
        <div className="col-5 h-100p">
          <Covnersations
            type={type}
            conversations={conversations}
            setConversations={setConversations}
          />
        </div>

        {/* Chatbox */}
        <div className="col-7">
          <ConversationBox handleRemoveConversation={handleRemoveConversation} type={type} />
        </div>
      </div>
    </section>
  );
};

export default Index;
