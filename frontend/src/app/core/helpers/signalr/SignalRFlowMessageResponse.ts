import { FlowMessageAction } from "../../entities/flow-messages/flow-message/enums/FlowMessageAction";
import { FlowMessageEventType } from "../../entities/flow-messages/flow-message/enums/FlowMessageEventType";
import { FlowMessageType } from "../../entities/flow-messages/flow-message/enums/FlowMessageType";
import { SignalRFlowMessageButtonResponse } from "./SignalRFlowMessageButtonResponse";
import { SignalRFlowMessageListItemResponse } from "./SignalRFlowMessageListItemResponse";

export interface SignalRFlowMessageResponse {
  content: string;
  type: FlowMessageType;
  action: FlowMessageAction;
  eventType: FlowMessageEventType;
  buttons: SignalRFlowMessageButtonResponse[];
  buttonListDisplayText: string;
  listItems: SignalRFlowMessageListItemResponse[];
}
