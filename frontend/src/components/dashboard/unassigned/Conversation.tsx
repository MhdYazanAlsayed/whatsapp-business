import ReactSelect from "src/components/shared/react-select/ReactSelect";
import ConversationProps from "./props/ConversationProps";
import { FormEvent, useState } from "react";
import DependenciesInjector from "src/app/core/util/DependenciesInjector";
import { useNavigate } from "react-router-dom";
import { ConversationOwner } from "src/app/core/enums/ConversationOwner";
import ConvertSelectOption from "../chat/conversation-box/messages-box/conversation-information/types/ConvertSelectOption";
import ConvertConversationRequest from "src/app/api/conversations/convert-conversation/ConvertConversationRequest";
import { toast } from "react-toastify";

const _conversationService = DependenciesInjector.services.conversationSerivce;
const Conversation = ({
  data,
  members,
  handleRemoveConversation,
}: ConversationProps) => {
  const [option, setOption] = useState<ConvertSelectOption | null>();

  const navigate = useNavigate();

  const handleOnChange = (x: ConvertSelectOption) => {
    setOption(x);
  };

  const handleTakeConversationAsync = async (
    e: FormEvent<HTMLButtonElement>
  ) => {
    e.preventDefault();

    const response = await _conversationService.takeAsync(
      data.id.value.toString()
    );

    if (response.succeeded) {
      navigate("/conversations");
    }
  };

  const handleOnSubmitAsync = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const request: ConvertConversationRequest = {
      to: option!.type,
      workGroupId:
        option!.type === ConversationOwner.WorkGroup
          ? option!.value
          : undefined,
      userId:
        option!.type === ConversationOwner.User ? option!.value : undefined,
    };

    const response = await _conversationService.convertAsync(
      data.id.value.toString(),
      request
    );
    if (!response.succeeded) return;

    toast.success(`تم تحويل المحادثة الى '${option!.label}'`);

    // Remove this conversation from here
    handleRemoveConversation(data.id.value);
  };

  return (
    <div className="conversation">
      {/* contact info */}
      <div className="contact-info">
        <div className="d-flex align-items-center gap-2">
          <div className="user-img">
            <svg
              width="100%"
              height="50"
              viewBox="0 0 51 50"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M25.5 45.8334C37.0059 45.8334 46.3333 36.506 46.3333 25.0001C46.3333 13.4941 37.0059 4.16675 25.5 4.16675C13.994 4.16675 4.66663 13.4941 4.66663 25.0001C4.66663 36.506 13.994 45.8334 25.5 45.8334Z"
                stroke="#535766"
                strokeWidth="2.5625"
              />
              <path
                d="M16.125 35.4166C20.9827 30.3287 29.965 30.0891 34.875 35.4166M30.6981 19.7916C30.6981 22.668 28.3629 24.9999 25.4823 24.9999C22.6019 24.9999 20.2666 22.668 20.2666 19.7916C20.2666 16.9151 22.6019 14.5833 25.4823 14.5833C28.3629 14.5833 30.6981 16.9151 30.6981 19.7916Z"
                stroke="#535766"
                strokeWidth="2.5625"
                strokeLinecap="round"
              />
            </svg>
          </div>
          <div className="user-name">
            <p>{data.contact?.fullName}</p>
            <small>انقر لعرض المحادثة</small>
          </div>
        </div>
      </div>
      <form
        onSubmit={handleOnSubmitAsync}
        className="select-box d-flex align-items-center gap-2"
      >
        <ReactSelect
          options={members}
          onChange={(x) => handleOnChange(x as ConvertSelectOption)}
        />
        <button className="custom-btn primary" type="submit" disabled={!option}>
          تعيين
        </button>
        <button
          onClick={handleTakeConversationAsync}
          className="custom-btn primary"
        >
          التحدث
        </button>
      </form>
    </div>
  );
};

export default Conversation;
