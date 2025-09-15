import { ConversationOwner } from "src/app/core/enums/ConversationOwner";
import Controls from "./Controls";
import ConversationBoxProps from "./props/ConversationBoxProps";
import MessagesBox from "./messages-box/MessagesBox";
import DependenciesInjector from "src/app/core/util/DependenciesInjector";
import { Fragment, useEffect, useState } from "react";
import Conversation from "src/app/core/entities/conversations/Conversation";
import Message from "src/app/core/entities/messages/Message";
import { useParams } from "react-router-dom";
import TemplateModal from "./templates-modal/TemplateModal";
import NotesModal from "./notes-modal/NotesModal";
import ConversationNote from "src/app/core/entities/conversations/conversation-notes/ConversationNote";
import ReplyTemplatesModal from "./reply-templates/ReplyTemplatesModal";
import { MessageType } from "src/app/core/entities/messages/enums/MessageType";

const _conversationService = DependenciesInjector.services.conversationSerivce;
const _signalRService = DependenciesInjector.services.singalRService;

let pages = 0;
const signalRConnectionKey = "chat";

const ConversationBox = ({
  type,
  handleRemoveConversation,
}: ConversationBoxProps) => {
  _signalRService.build(signalRConnectionKey, "chat-conversation");

  const [conversation, setConversation] = useState<Conversation | null>(null);
  const [templatesModal, setTemplatesModal] = useState(false);
  const [replyTemplatesModal, setReplyTemplatesModal] = useState(false);
  const [notesModal, setNotesModal] = useState(false);
  const [disableControls, setDisableControls] = useState(false);
  const { id } = useParams();

  useEffect(() => {
    handleGetDetailsAsync();

    if (!id) {
      handleDisconnectSignalRAsync();

      return;
    }

    handleConnectSignalRAsync();

    () => handleDisconnectSignalRAsync();
  }, [id]);

  useEffect(() => {
    if (!conversation) return;

    handleCheckLastMessageToEnableControls(conversation.messages!);
  }, [conversation]);

  const handleConnectSignalRAsync = async () => {
    await _signalRService.connectionAsync(signalRConnectionKey);
  };

  const handleDisconnectSignalRAsync = async () => {
    await _signalRService.disconnectionAsync(signalRConnectionKey);
  };

  const handleGetDetailsAsync = async () => {
    if (!id) return;
    const response = await _conversationService.detailsAsync(id);
    pages = response.messagePages;

    response.conversation.messages = response.messages;

    console.log(response.conversation);
    setConversation(response.conversation);
  };

  if (!id || !conversation) {
    return (
      <div className="conversation h-100p d-flex flex-column gap-4">
        <div className="messages-box rounded-4 overflow-hidden"></div>

        <Controls disabled />
      </div>
    );
  }

  // ? This function in order to add more messages to our state
  const handleAddMoreMessages = (data: Message[]) => {
    setConversation((prev: any) => {
      (prev as Conversation).messages = [...prev.messages, ...data];

      return { ...prev };
    });
  };

  const handleSendMessage = (primaryKey: string, message: string) => {
    const msg: Message = {
      id: null!,
      createdBy: primaryKey,
      conversationId: { value: parseInt(id) },
      createdAt: new Date().toString(),
      isNotify: false,
      received: false,
      type: MessageType.Text,
      content: message,
      conversation: null!,
      flowMessage: null!,
      flowMessageId: null!,
      history: null!,
    };

    setConversation((prev) => {
      if (!prev) return prev;

      prev.messages!.push(msg);

      return { ...prev };
    });
  };

  const handleCancleSendMessage = (primaryKey: string) => {
    setConversation((prev) => {
      if (!prev) return prev;

      const index = prev.messages!.findIndex((x) => x.createdBy == primaryKey);
      if (index === -1) throw new Error();

      prev.messages!.splice(index, 1);

      return { ...prev };
    });
  };

  const handleUpdateConversationNote = (note: ConversationNote) => {
    setConversation((prev: any) => {
      prev.note = note;

      return { ...prev };
    });
  };

  const handleCheckLastMessageToEnableControls = (data: Message[]) => {
    const messages = data
      .filter((x) => x.received)
      .sort(
        (a, b) =>
          new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
      );

    if (messages.length == 0) {
      setDisableControls(true);
      return;
    }

    const lastMessageDate = new Date(messages[0].createdAt);
    const x = new Date();

    const difference = x.getTime() - lastMessageDate.getTime(); // الفرق بالمللي ثانية

    // تحويل الفرق إلى ساعات
    const differenceInHours = difference / (1000 * 60 * 60);

    if (Math.floor(differenceInHours) < 24) {
      setDisableControls(false);
      return;
    }

    setDisableControls(true);
  };

  return (
    <div className="conversation h-100p d-flex flex-column">
      <Fragment>
        <MessagesBox
          pages={pages}
          conversation={conversation}
          handleAddMoreMessages={handleAddMoreMessages}
          handleRemoveConversation={handleRemoveConversation}
          signalRConnectionKey={signalRConnectionKey}
        />

        {/* Buttons */}
        <div className="buttons d-flex align-items-center gap-2 py-3">
          <button
            className="custom-btn py-1 primary rounded"
            disabled={type == ConversationOwner.Bot}
          >
            الرد
          </button>
          <button
            className="custom-btn py-1 secondary rounded"
            disabled={type == ConversationOwner.Bot}
            onClick={() => setNotesModal(true)}
          >
            اترك ملاحظة
          </button>
          <button
            className="custom-btn py-1 secondary rounded"
            disabled={type == ConversationOwner.Bot}
            onClick={() => setReplyTemplatesModal(true)}
          >
            الردود الجاهزة
          </button>
          <button
            className="custom-btn py-1 secondary rounded"
            disabled={type == ConversationOwner.Bot}
            onClick={() => setTemplatesModal(true)}
          >
            القوالب
          </button>
        </div>

        <Controls
          id={id}
          handleSendMessage={handleSendMessage}
          handleCancleSendMessage={handleCancleSendMessage}
          disabled={type == ConversationOwner.Bot || disableControls}
        />
      </Fragment>

      {/* Modals */}
      <TemplateModal
        open={templatesModal}
        setOpen={setTemplatesModal}
        handleAddMoreMessages={handleAddMoreMessages}
      />

      <NotesModal
        id={conversation.id}
        open={notesModal}
        setOpen={setNotesModal}
        note={conversation.note}
        handleUpdateConversationNote={handleUpdateConversationNote}
      />

      <ReplyTemplatesModal
        open={replyTemplatesModal}
        setOpen={setReplyTemplatesModal}
        handleSendMessage={handleSendMessage}
        handleCancleSendMessage={handleCancleSendMessage}
      />
    </div>
  );
};

export default ConversationBox;
