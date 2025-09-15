import { useEffect, useState } from "react";
import { Navigate, useParams } from "react-router-dom";
import Conversation from "src/app/core/entities/conversations/Conversation";
import WorkGroups from "./WorkGroups";
import DependenciesInjector from "src/app/core/util/DependenciesInjector";
import Loader from "src/components/shared/Loader";
import ConversationCP from "./Conversation";
import SelectOption from "src/app/core/helpers/SelectOption";
import ConvertSelectOption from "../chat/conversation-box/messages-box/conversation-information/types/ConvertSelectOption";

const _conversationService = DependenciesInjector.services.conversationSerivce;
const _workGroupSerive = DependenciesInjector.services.workGroupService;
const _convertOptionService =
  DependenciesInjector.services.convertOptionService;

const UnAssignedChats = () => {
  const { groupId } = useParams();
  if (!groupId) return <Navigate to={"/errors/404"} />;
  const isCustomerService = groupId.trim() == "customer-service";

  const [conversations, setConversations] = useState<Conversation[]>([]);
  const [loading, setLoading] = useState(false);
  const [members, setMembers] = useState<SelectOption[]>([
    { value: "-1", label: "البوت" },
  ]);

  useEffect(() => {
    handleGetConversationsAsync();
    handleGetMembersAsync();
  }, [groupId]);

  // Get Conversations
  const handleGetConversationsAsync = async () => {
    setLoading(true);

    if (isCustomerService) {
      const customerServiceConversation =
        await _conversationService.getCustomerServiceAsync(1);
      setConversations(customerServiceConversation.data);
    } else {
      const workGroupConversations =
        await _conversationService.getWorkGroupConversationsAsync(groupId, 1);

      setConversations(workGroupConversations.data);
    }

    setLoading(false);
  };

  const handleGetMembersAsync = async () => {
    if (isCustomerService) {
      const allOptions = await _convertOptionService.getAsync();
      setMembers(
        allOptions.map(
          (x) =>
            ({
              value: x.id,
              label: x.text,
              type: x.type,
            } as ConvertSelectOption)
        )
      );
      return;
    }

    const response = await _workGroupSerive.getMembersAsync(groupId);

    setMembers(
      response.map(
        (x) =>
          ({
            value: x.id.value.toString(),
            label: x.arabicName,
          } as SelectOption)
      )
    );
  };

  const handleRemoveConversation = (id: number) => {
    setConversations((prev) => {
      const index = prev.findIndex((x) => x.id.value == id);
      if (index === -1) throw new Error();

      prev.splice(index, 1);
      return [...prev];
    });
  };

  return (
    <section className="unassigned-conversations d-flex flex-column gap-4">
      <WorkGroups />

      <div className="conversations-box">
        <div className="tabs">
          <button className="active">
            <svg
              width="17"
              height="17"
              viewBox="0 0 17 17"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M8.5 4.25V14.1667"
                stroke="#646D74"
                strokeWidth="1.5"
                strokeLinecap="round"
              />
              <path
                d="M4.23619 2.32719C6.60286 2.7778 8.0134 3.72039 8.49996 4.26153C8.98651 3.72039 10.397 2.7778 12.7637 2.32719C13.9627 2.0989 14.5622 1.98476 15.0727 2.42225C15.5833 2.85973 15.5833 3.57016 15.5833 4.991V10.0973C15.5833 11.3964 15.5833 12.046 15.2556 12.4516C14.9279 12.8571 14.2066 12.9944 12.7637 13.2692C11.4776 13.5141 10.4738 13.9042 9.74719 14.2964C9.03234 14.6821 8.67492 14.875 8.49996 14.875C8.325 14.875 7.96757 14.6821 7.25273 14.2964C6.52615 13.9042 5.52236 13.5141 4.23619 13.2692C2.79338 12.9944 2.07198 12.8571 1.7443 12.4516C1.41663 12.046 1.41663 11.3964 1.41663 10.0973V4.991C1.41663 3.57016 1.41663 2.85973 1.92718 2.42225C2.43774 1.98476 3.03722 2.0989 4.23619 2.32719Z"
                stroke="#646D74"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
            المحادثات المفتوحة
          </button>
          <button disabled>
            <svg
              width="17"
              height="18"
              viewBox="0 0 17 18"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M3.56995 4.23975L13.0899 13.7597"
                stroke="#646D74"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
              <path
                d="M15.13 8.99971C15.13 5.24417 12.0855 2.19971 8.33003 2.19971C4.57449 2.19971 1.53003 5.24417 1.53003 8.99971C1.53003 12.7552 4.57449 15.7997 8.33003 15.7997C12.0855 15.7997 15.13 12.7552 15.13 8.99971Z"
                stroke="#646D74"
                strokeWidth="1.5"
              />
            </svg>
            المحادثات المغلقة
          </button>
          <button disabled>
            <svg
              width="17"
              height="18"
              viewBox="0 0 17 18"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M15.13 8.99971C15.13 5.24417 12.0855 2.19971 8.33003 2.19971C4.57449 2.19971 1.53003 5.24417 1.53003 8.99971C1.53003 12.7552 4.57449 15.7997 8.33003 15.7997C12.0855 15.7997 15.13 12.7552 15.13 8.99971Z"
                stroke="#646D74"
                strokeWidth="1.5"
              />
            </svg>
            المحادثات المستلمة
          </button>
        </div>
        {/* Search boxes */}
        <div className="d-flex align-items-center gap-2 mb-3">
          <label className="custom-input search-box">
            <svg
              width="17"
              height="16"
              viewBox="0 0 18 19"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M13.125 13.625L16.5 17"
                stroke="#6E6E6E"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
              <path
                d="M15 8.75C15 5.02208 11.978 2 8.25 2C4.52208 2 1.5 5.02208 1.5 8.75C1.5 12.478 4.52208 15.5 8.25 15.5C11.978 15.5 15 12.478 15 8.75Z"
                stroke="#6E6E6E"
                strokeWidth="1.5"
                strokeLinejoin="round"
              />
            </svg>

            <input
              className=""
              type="text"
              placeholder="ابحث عن طريق الأسم ..."
            />
          </label>
          <label className="custom-input search-box">
            <svg
              width="17"
              height="16"
              viewBox="0 0 18 19"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M13.125 13.625L16.5 17"
                stroke="#6E6E6E"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
              <path
                d="M15 8.75C15 5.02208 11.978 2 8.25 2C4.52208 2 1.5 5.02208 1.5 8.75C1.5 12.478 4.52208 15.5 8.25 15.5C11.978 15.5 15 12.478 15 8.75Z"
                stroke="#6E6E6E"
                strokeWidth="1.5"
                strokeLinejoin="round"
              />
            </svg>

            <input
              className=""
              type="text"
              placeholder="ابحث عن طريق الأسم ..."
            />
          </label>
        </div>
        {/* Conversations container */}
        <div
          className={`conversations-container ${
            conversations.length == 0 || loading
              ? "d-flex flex-column justify-content-center"
              : ""
          }`}
        >
          {loading ? (
            <Loader />
          ) : conversations.length == 0 ? (
            <div className="text-center text-muted">لا توجد محادثات</div>
          ) : (
            conversations.map((x, index) => (
              <ConversationCP
                data={x}
                key={index}
                members={members}
                handleRemoveConversation={handleRemoveConversation}
              />
            ))
          )}
        </div>
      </div>
    </section>
  );
};

export default UnAssignedChats;
