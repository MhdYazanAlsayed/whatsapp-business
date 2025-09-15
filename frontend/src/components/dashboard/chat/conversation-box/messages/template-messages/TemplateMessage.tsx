import Props from "./props/Props";
import HeaderFileName from "./HeaderFileName";

const TemplateMessage = ({ data, sender }: Props) => {
  return (
    <div className="customer-service message">
      <div className="message-img">
        <svg
          width="40"
          height="40"
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
        <small className="customer-employee-name text-muted">
          {sender?.arabicName ?? " - "}
        </small>

        <div className="message-text template-message p-2">
          {data.headerFileName && (
            <HeaderFileName fileName={data.headerFileName} />
          )}
          <p className="">{data?.body}</p>
          <span className="time">
            {new Date(data.createdAt).toLocaleTimeString()}
          </span>
        </div>
      </div>
    </div>
  );
};

export default TemplateMessage;
