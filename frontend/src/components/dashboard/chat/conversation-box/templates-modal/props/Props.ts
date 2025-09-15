import Message from "src/app/core/entities/messages/Message";
import TadawiModalProps from "src/app/core/helpers/TadawiModalProps";

export default interface Props extends TadawiModalProps {
  handleAddMoreMessages(data: Message[]): void;
}
