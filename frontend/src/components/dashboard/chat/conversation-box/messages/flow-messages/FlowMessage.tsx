import { FlowMessageType } from "src/app/core/entities/flow-messages/flow-message/enums/FlowMessageType";
import FlowMessageButtons from "./FlowMessageButtons";
import FlowMessageList from "./FlowMessageList";
import FlowMessageProps from "./props/FlowMessageProps";
import FlowMessageNone from "./FlowMessageNone";

const FlowMessage = ({ message, sender }: FlowMessageProps) => {
  if (message.type == FlowMessageType.Buttons)
    return <FlowMessageButtons message={message} />;

  if (message.type == FlowMessageType.List)
    return <FlowMessageList message={message} sender={sender} />;

  if (message.type == FlowMessageType.None)
    return <FlowMessageNone message={message} sender={sender} />;

  throw new Error("");
};

export default FlowMessage;
