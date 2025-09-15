import { useEffect, useState } from "react";
import Contact from "./Contact";
import DependenciesInjector from "src/app/core/util/DependenciesInjector";
import ConversationProps from "../props/ConversationsProps";
import Loader from "src/components/shared/Loader";

const _conversationService = DependenciesInjector.services.conversationSerivce;
const pagination = { currentPage: 1, pages: 0 };

const Covnersations = ({
  type,
  conversations,
  setConversations,
}: ConversationProps) => {
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    handleGetConversationsAsync();

    const interval = setInterval(handleGetConversationsAsync, 60 * 1000);

    return () => clearInterval(interval);
  }, []);

  const handleGetConversationsAsync = async () => {
    const response = await _conversationService.getAsync(1, type);

    pagination.pages = response.pages;
    setConversations(response.data);
    setLoading(false);
  };

  return (
    <div className="contacts h-100p p-3">
      <p className="pb-3 fw-semibold">محادثات اليوم</p>

      <label className="custom-input search-box mb-2">
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
          placeholder="ابحث عن طريق الأسم او التاريخ..."
        />
      </label>

      <div className="contacts-container" dir="ltr">
        {conversations.length == 0 ? (
          loading ? (
            <Loader />
          ) : (
            <div className="h-100p d-flex flex align-items-center">
              <p className="text-center w-100 text-muted">
                لا توجد محادثات بعد
              </p>
            </div>
          )
        ) : (
          conversations.map((x, index) => (
            <Contact conversation={x} key={index} />
          ))
        )}
      </div>
    </div>
  );
};

export default Covnersations;
