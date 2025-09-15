import Background from "src/assets/images/Background.png";
import MessagesBoxProps from "../props/MessagesBoxProps";
import Message from "../messages/Message";
import { useEffect, useRef, useState } from "react";
import Loader from "src/components/shared/Loader";
import { useParams } from "react-router-dom";
import DependenciesInjector from "src/app/core/util/DependenciesInjector";
import ConversationInformation from "./conversation-information/ConversationInformation";
import SignalRMessageResponse from "src/app/core/helpers/signalr/SignalRMessageResponse";
import { RealtimeEvents } from "src/app/core/helpers/RealtimeNotifyType";
import FlowMessageButton from "src/app/core/entities/flow-messages/flow-message-buttons/FlowMessageButton";
import FlowMessageListItem from "src/app/core/entities/flow-messages/flow-message-list/FlowMessageListItem";
import FlowMessage from "src/app/core/entities/flow-messages/flow-message/FlowMessage";

const _conversationService = DependenciesInjector.services.conversationSerivce;
const _signalRService = DependenciesInjector.services.singalRService;

const MessagesBox = ({
  conversation,
  pages,
  handleAddMoreMessages,
  handleRemoveConversation,
  signalRConnectionKey,
}: MessagesBoxProps) => {
  const messagesPagination = { pages: pages, currentPage: 1 };

  const [loading, setLoading] = useState(false);
  const messagesRef = useRef<HTMLDivElement | null>(null);

  const { id } = useParams();

  useEffect(() => {
    _signalRService.listen(
      signalRConnectionKey,
      RealtimeEvents.NewMessage,
      handleReceiveNewMessage
    );

    () => _signalRService.off(signalRConnectionKey, RealtimeEvents.NewMessage);
  }, []);

  useEffect(() => {
    addScrollEventListener();

    () => removeScrollEventListener();
  }, [id]);

  const handleOnScroll = () => {
    let value =
      messagesRef.current!.scrollTop - messagesRef.current!.offsetHeight;

    // Convert to positive value
    value = value * -1;

    if (value >= messagesRef.current!.scrollHeight - 165) {
      handleFetchMessagesAsync();
    }
  };

  // ? This function will run when the scroll reach to end
  const handleFetchMessagesAsync = async () => {
    removeScrollEventListener();

    if (!handleCheckToGetMoreMessages()) return;

    setLoading(true);
    const messages = await handleGetMessagesAsync();
    if (!messages) return;

    handleAddMoreMessages(messages.data);
    setLoading(false);

    addScrollEventListener();
  };

  // ? This function in order to get more messages
  const handleGetMessagesAsync = async () => {
    const response = await _conversationService.fetchMessagesAsync(
      id!,
      ++messagesPagination.currentPage
    );
    if (!response) return null;

    return response;
  };

  // ? This function in order to check if there is more messages to load .
  const handleCheckToGetMoreMessages = () => {
    return messagesPagination.currentPage < messagesPagination.pages;
  };

  const removeScrollEventListener = () => {
    messagesRef.current!.removeEventListener("scroll", handleOnScroll);
  };

  const addScrollEventListener = () => {
    messagesRef?.current?.addEventListener("scroll", handleOnScroll);
  };

  // ? This function will execute when signalR receive a new message
  const handleReceiveNewMessage = (data: SignalRMessageResponse) => {
    handleAddMoreMessages([
      {
        conversationId: data.conversationId,
        createdAt: data.createdAt,
        id: null!,
        isNotify: data.isNotify,
        received: data.received,
        type: data.type,
        content: data.content,
        conversation: null!,
        flowMessage:
          data.flowMessage === null
            ? undefined
            : ({
                action: data.flowMessage?.action!,
                content: data.flowMessage?.content,
                createdAt: new Date().toString()!,
                eventType: data.flowMessage?.eventType!,
                id: null!,
                type: data.flowMessage?.type!,
                buttonListDisplayText: data.flowMessage?.buttonListDisplayText!,
                buttons: data.flowMessage?.buttons.map(
                  (x) =>
                    ({
                      displayText: x.displayText,
                      flowMessageId: null!,
                      id: x.id!,
                      next: x.next,
                      flowMessage: null!,
                    } as FlowMessageButton)
                ),
                listItems: data.flowMessage?.listItems.map(
                  (x) =>
                    ({
                      content: x.content,
                      flowMessageId: null!,
                      id: x.id,
                      next: x.next,
                      description: x.description,
                      flowMessage: null!,
                    } as FlowMessageListItem)
                ),
              } as FlowMessage),
      },
    ]);
  };

  return (
    <div
      className="messages-box rounded-4 overflow-hidden"
      style={{ backgroundImage: `url(${Background})` }}
    >
      {/* Conversation Info */}
      <ConversationInformation
        id={id!}
        owner={conversation.owner}
        fullName={conversation.contact?.fullName!}
        handleRemoveConversation={handleRemoveConversation}
        phoneNumber={conversation.contact?.phoneNumber!}
      />

      {/* Messages */}
      <div
        className="messages d-flex flex-column-reverse gap-3 overflow-auto"
        ref={messagesRef}
      >
        {conversation.messages
          ?.sort(
            (a, b) =>
              new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
          )
          .map((x, index) => (
            <Message {...x} key={index} />
          ))}
        {loading && (
          <div>
            <Loader />
          </div>
        )}
      </div>
    </div>
  );
};

export default MessagesBox;
