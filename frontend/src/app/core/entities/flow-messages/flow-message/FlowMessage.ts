import Entity from "../../base/Entity";
import FlowMessageButton from "../flow-message-buttons/FlowMessageButton";
import FlowMessageListItem from "../flow-message-list/FlowMessageListItem";
import { FlowMessageAction } from "./enums/FlowMessageAction";
import { FlowMessageEventType } from "./enums/FlowMessageEventType";
import { FlowMessageType } from "./enums/FlowMessageType";
import FlowMessageId from "./keys/FlowMessageId";

export default interface FlowMessage extends Entity {
  id: FlowMessageId;
  content: string;
  type: FlowMessageType;
  eventType: FlowMessageEventType;
  action: FlowMessageAction;
  buttons?: FlowMessageButton[];
  buttonListDisplayText?: string;
  listItems?: FlowMessageListItem[];
}
