import { FormEvent, useState } from "react";
import { ControlsProps } from "./props/ControlsProps";
import DependenciesInjector from "src/app/core/util/DependenciesInjector";
import { toast } from "react-toastify";

const _conversationService = DependenciesInjector.services.conversationSerivce;
const Controls = ({
  id,
  disabled,
  handleSendMessage,
  handleCancleSendMessage,
}: ControlsProps) => {
  const [message, setMessage] = useState("");

  if (disabled) {
    return (
      <div className="controls">
        <div className="type-message-disabled">
          <small className="text-white">اكتب رسالتك هنا ...</small>
          <div className="icon">
            <svg
              width="16"
              height="16"
              viewBox="0 0 16 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M3.89133 7.07762C5.62193 3.55675 6.48721 1.79635 7.89951 1.79633C9.31176 1.79635 10.1771 3.55679 11.9077 7.0776L13.0862 9.47526C14.4153 12.1792 15.0798 13.5312 14.5966 14.1813C14.4668 14.356 14.2956 14.5002 14.0965 14.6025C13.356 14.9831 11.9966 14.2164 9.2778 12.6829C8.67475 12.3427 8.3732 12.1726 8.04338 12.1422C7.94769 12.1334 7.85133 12.1334 7.75564 12.1422C7.42582 12.1726 7.12427 12.3427 6.52125 12.6828C3.80244 14.2164 2.44303 14.9831 1.70251 14.6025C1.50341 14.5002 1.33223 14.356 1.20233 14.1813C0.719175 13.5312 1.38372 12.1792 2.71282 9.47525L3.89133 7.07762Z"
                stroke="#FAFAFA"
                strokeWidth="2"
              />
            </svg>
          </div>
        </div>
      </div>
    );
  }

  const handleOnSubmitAsync = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const msg = message;
    setMessage("");

    if (!handleSendMessage || !handleCancleSendMessage || !id)
      throw new Error();

    const date = Date().toString();
    const primaryKey = "System_" + message + "_" + date;

    handleSendMessage(primaryKey, message);

    const response = await _conversationService.sendMessageAsync(id, msg);

    if (!response.succeeded) {
      // Remove the message
      toast.error(`فشل ارسال الرسالة '${message}'`);
      handleCancleSendMessage(primaryKey);
    }
  };

  return (
    <div className="controls">
      <form onSubmit={handleOnSubmitAsync} className="type-message">
        <input
          type="text"
          placeholder="اكتب رسالتك هنا ..."
          value={message}
          onChange={(x) => setMessage(x.currentTarget.value)}
        />
        <div className="d-flex align-items-center gap-2">
          <div className="icon">
            <svg
              width="21"
              height="20"
              viewBox="0 0 21 20"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M7.4657 6.66748V5.00081C7.4657 3.15986 8.95808 1.66748 10.799 1.66748C12.6399 1.66748 14.1324 3.15986 14.1324 5.00081V15.0008C14.1324 16.8417 12.6399 18.3342 10.799 18.3342C8.95808 18.3342 7.4657 16.8417 7.4657 15.0008V11.2508C7.4657 10.1002 8.39844 9.16749 9.54903 9.16749C10.6996 9.16749 11.6324 10.1002 11.6324 11.2508V13.3342"
                stroke="#646D74"
                strokeWidth="1.2"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
          </div>
          <div className="icon">
            <svg
              width="21"
              height="20"
              viewBox="0 0 21 20"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M11.6324 2.50193C11.2406 2.5 10.8246 2.5 10.3824 2.5C6.65041 2.5 4.78444 2.5 3.62506 3.65937C2.4657 4.81874 2.4657 6.68472 2.4657 10.4167C2.4657 14.1486 2.4657 16.0146 3.62506 17.174C4.78444 18.3333 6.65041 18.3333 10.3824 18.3333C14.1143 18.3333 15.9803 18.3333 17.1397 17.174C18.255 16.0586 18.2974 14.2892 18.2989 10.8333"
                stroke="#646D74"
                strokeWidth="1.2"
                strokeLinecap="round"
              />
              <path
                d="M2.4657 11.7793C2.98155 11.7044 3.50306 11.6674 4.02546 11.6687C6.23541 11.622 8.39123 12.3106 10.1083 13.6117C11.7007 14.8182 12.8196 16.4789 13.299 18.3332"
                stroke="#646D74"
                strokeWidth="1.2"
                strokeLinejoin="round"
              />
              <path
                d="M18.299 14.0803C17.3195 13.5842 16.3063 13.3325 15.2875 13.3335C13.7444 13.3275 12.2169 13.8945 10.799 15.0001"
                stroke="#646D74"
                strokeWidth="1.2"
                strokeLinejoin="round"
              />
              <path
                d="M14.9657 3.74984C15.3753 3.32842 16.4655 1.6665 17.049 1.6665M17.049 1.6665C17.6325 1.6665 18.7228 3.32842 19.1324 3.74984M17.049 1.6665V8.33317"
                stroke="#646D74"
                strokeWidth="1.2"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
          </div>
          <div className="icon">
            <svg
              width="21"
              height="20"
              viewBox="0 0 21 20"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M15.799 15C16.8362 15.3537 17.4657 15.8183 17.4657 16.3271C17.4657 17.4351 14.4809 18.3333 10.799 18.3333C7.11709 18.3333 4.13232 17.4351 4.13232 16.3271C4.13232 15.8183 4.76172 15.3537 5.79899 15"
                stroke="#646D74"
                strokeWidth="1.2"
                strokeLinecap="round"
              />
              <path
                d="M13.299 7.9165C13.299 9.29725 12.1798 10.4165 10.799 10.4165C9.41826 10.4165 8.29901 9.29725 8.29901 7.9165C8.29901 6.5358 9.41826 5.4165 10.799 5.4165C12.1798 5.4165 13.299 6.5358 13.299 7.9165Z"
                stroke="#646D74"
                strokeWidth="1.2"
              />
              <path
                d="M10.799 1.6665C14.1813 1.6665 17.049 4.5232 17.049 7.98892C17.049 11.5098 14.1347 13.9807 11.4428 15.6608C11.2466 15.7738 11.0248 15.8332 10.799 15.8332C10.5733 15.8332 10.3514 15.7738 10.1553 15.6608C7.46839 13.9643 4.54901 11.522 4.54901 7.98892C4.54901 4.5232 7.41668 1.6665 10.799 1.6665Z"
                stroke="#646D74"
                strokeWidth="1.2"
              />
            </svg>
          </div>
          <div className="icon">
            <svg
              width="21"
              height="20"
              viewBox="0 0 21 20"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M14.9657 5.83317V9.1665C14.9657 11.4677 13.1002 13.3332 10.799 13.3332C8.49781 13.3332 6.63232 11.4677 6.63232 9.1665V5.83317C6.63232 3.53199 8.49781 1.6665 10.799 1.6665C13.1002 1.6665 14.9657 3.53199 14.9657 5.83317Z"
                stroke="#646D74"
                strokeWidth="1.2"
              />
              <path
                d="M14.9657 5.8335H12.4657M14.9657 9.16683H12.4657"
                stroke="#646D74"
                strokeWidth="1.2"
                strokeLinecap="round"
              />
              <path
                d="M17.4657 9.1665C17.4657 12.8484 14.4809 15.8332 10.799 15.8332M10.799 15.8332C7.11709 15.8332 4.13232 12.8484 4.13232 9.1665M10.799 15.8332V18.3332M10.799 18.3332H13.299M10.799 18.3332H8.29899"
                stroke="#646D74"
                strokeWidth="1.2"
                strokeLinecap="round"
              />
            </svg>
          </div>
          <div
            className={`send icon ${message.trim() === "" ? "disabled" : ""}`}
          >
            <svg
              width="16"
              height="16"
              viewBox="0 0 16 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M3.89133 7.07762C5.62193 3.55675 6.48721 1.79635 7.89951 1.79633C9.31176 1.79635 10.1771 3.55679 11.9077 7.0776L13.0862 9.47526C14.4153 12.1792 15.0798 13.5312 14.5966 14.1813C14.4668 14.356 14.2956 14.5002 14.0965 14.6025C13.356 14.9831 11.9966 14.2164 9.2778 12.6829C8.67475 12.3427 8.3732 12.1726 8.04338 12.1422C7.94769 12.1334 7.85133 12.1334 7.75564 12.1422C7.42582 12.1726 7.12427 12.3427 6.52125 12.6828C3.80244 14.2164 2.44303 14.9831 1.70251 14.6025C1.50341 14.5002 1.33223 14.356 1.20233 14.1813C0.719175 13.5312 1.38372 12.1792 2.71282 9.47525L3.89133 7.07762Z"
                stroke="#FAFAFA"
                strokeWidth="2"
              />
            </svg>
          </div>
        </div>
      </form>
    </div>
  );
};

export default Controls;
