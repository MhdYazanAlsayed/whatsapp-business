import FlowMessage from "src/app/core/entities/flow-messages/flow-message/FlowMessage";
import Employee from "src/app/core/entities/employees/Employee";

export default interface FlowMessageProps {
  message: FlowMessage;
  sender?: Employee;
}
