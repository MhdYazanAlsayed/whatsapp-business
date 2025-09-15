import Props from "./props/Props";

const ReplyItem = ({ data, onClick }: Props) => {
  return (
    <div
      className="p-2 border-bottom cursor-pointer"
      onClick={() => onClick(data)}
    >
      <div className="response">
        <p className="fw-semibold">{data.title}</p>
        <p className="response-text">{data.content.slice(0, 60)}</p>
      </div>
    </div>
  );
};

export default ReplyItem;
