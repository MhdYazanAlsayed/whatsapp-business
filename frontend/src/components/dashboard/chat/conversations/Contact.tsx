import { Link } from "react-router-dom";
import Conversation from "src/app/core/entities/conversations/Conversation";
import { ConversationOwner } from "src/app/core/enums/ConversationOwner";

const Contact = (props: { conversation: Conversation }) => {
  const link =
    props.conversation.owner == ConversationOwner.Bot
      ? "/bot/conversations"
      : props.conversation.owner == ConversationOwner.User
      ? "/conversations"
      : "";

  return (
    <Link
      to={`${link}/${props.conversation.id.value}`}
      className="contact"
      dir="rtl"
    >
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
        <div className="user-name d-flex flex-column">
          <p className="">{props.conversation.contact?.fullName}</p>
          <small>انقر لعرض المحادثة</small>
        </div>
      </div>
      {/* <div className="department">
        <p>قسم</p>
        <p>( طب الأسنان )</p>
      </div> */}
    </Link>
  );
};

export default Contact;
