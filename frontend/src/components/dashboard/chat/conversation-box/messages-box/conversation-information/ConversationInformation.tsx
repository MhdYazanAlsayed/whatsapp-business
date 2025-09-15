import { FormEvent, useEffect, useState } from "react";
import ReactSelect from "src/components/shared/react-select/ReactSelect";
import ConversationInformationProps from "../../props/ConversationInformationProps";
import DependenciesInjector from "src/app/core/util/DependenciesInjector";
import ConvertSelectOption from "./types/ConvertSelectOption";
import { useNavigate } from "react-router-dom";
import ConvertConversationRequest from "src/app/api/conversations/convert-conversation/ConvertConversationRequest";
import { ConversationOwner } from "src/app/core/enums/ConversationOwner";
import { toast } from "react-toastify";

// Inject services
const _conversationService = DependenciesInjector.services.conversationSerivce;

// Component
const ConversationInformation = ({
  id,
  fullName,
  owner,
  phoneNumber,
  handleRemoveConversation,
}: ConversationInformationProps) => {
  const [options, setOptions] = useState<ConvertSelectOption[]>([]);
  const [currentOption, setCurrentOption] =
    useState<ConvertSelectOption | null>();

  const navigate = useNavigate();

  useEffect(() => {
    handleGetOptionsAsync();
  }, [id]);

  const handleGetOptionsAsync = async () => {
    const result = await _conversationService.getConvertOptionsAsync(id);

    setOptions(
      result.map(
        (x) =>
          ({
            value: x.id,
            label: x.text,
            type: x.type,
          } as ConvertSelectOption)
      )
    );
  };

  const handleChangeSelect = (x: ConvertSelectOption) => {
    setCurrentOption(x);
  };

  const handleOnSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!currentOption) throw new Error();

    const userId =
      currentOption.type == ConversationOwner.User
        ? currentOption.value
        : undefined;

    const workGroupId =
      currentOption.type == ConversationOwner.WorkGroup
        ? currentOption.value
        : undefined;

    handleCovnertCovnersationAsync(
      currentOption.type,
      userId,
      workGroupId,
      currentOption.label
    );

    // const data: ConvertConversationRequest = {
    //   to: currentOption.type,
    //   userId:
    //     currentOption.type == ConversationOwner.User
    //       ? currentOption.value
    //       : undefined,
    //   workGroupId:
    //     currentOption.type == ConversationOwner.WorkGroup
    //       ? currentOption.value
    //       : undefined,
    // };
    // const response = await _conversationService.convertAsync(id, data);
    // if (!response.succeeded) return;

    // let path = window.location.pathname;
    // path = path.slice(0, path.lastIndexOf("/"));
    // navigate(path);
    // toast.success(`تم تعيين المحادثة الى '${currentOption.label}'`);
    // handleRemoveConversation(parseInt(id));
  };

  const handleTakeConversationAsync = async (
    e: FormEvent<HTMLButtonElement>
  ) => {
    e.preventDefault();

    const response = await _conversationService.takeAsync(id);
    if (!response.succeeded) return;

    navigate(`/conversations/${id}`);
  };

  const handleCloseConversation = (e: FormEvent<HTMLButtonElement>) => {
    e.preventDefault();

    handleCovnertCovnersationAsync(
      ConversationOwner.Bot,
      undefined,
      undefined,
      "البوت"
    );
  };

  const handleCovnertCovnersationAsync = async (
    type: ConversationOwner,
    userId?: string,
    workGroupId?: string,
    label?: string
  ) => {
    const data: ConvertConversationRequest = {
      to: type,
      userId: userId,
      workGroupId: workGroupId,
    };
    const response = await _conversationService.convertAsync(id, data);
    if (!response.succeeded) return;

    let path = window.location.pathname;
    path = path.slice(0, path.lastIndexOf("/"));
    navigate(path);
    if (label) {
      toast.success(`تم تعيين المحادثة الى '${label}'`);
    }
    handleRemoveConversation(parseInt(id));
  };
  return (
    <div className="conversation-info rounded-4 p-2">
      <div className="d-flex align-items-center justify-content-between">
        <ContactInfo fullName={fullName} phoneNumber={phoneNumber} />

        <form
          onSubmit={handleOnSubmit}
          className="info-select d-flex align-items-center gap-2"
        >
          <ReactSelect
            options={options}
            onChange={(x) => handleChangeSelect(x as any)}
          />

          <button
            className="custom-btn primary small py-1 px-4 rounded"
            type="submit"
          >
            تعيين
          </button>

          {owner != ConversationOwner.User ? (
            <button
              className="custom-btn primary small py-1 px-4 rounded"
              onClick={handleTakeConversationAsync}
            >
              التحدث
            </button>
          ) : (
            <button
              className="custom-btn secondary small py-1 px-4 rounded"
              onClick={handleCloseConversation}
            >
              اغلاق
            </button>
          )}
        </form>
      </div>
    </div>
  );
};

const ContactInfo = ({
  fullName,
  phoneNumber,
}: {
  fullName: string;
  phoneNumber: string;
}) => {
  return (
    <div className="d-flex align-items-center gap-2">
      <div className="user-img">
        <svg
          width="50px"
          height="100%"
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
      <div>
        <p className="text-muted small">{fullName}</p>
        <small className="text-muted">{phoneNumber + "+"}</small>
      </div>
    </div>
  );
};

export default ConversationInformation;
