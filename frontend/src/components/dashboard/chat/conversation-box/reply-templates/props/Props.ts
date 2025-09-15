import TadawiModalProps from "src/app/core/helpers/TadawiModalProps";

export default interface Props extends TadawiModalProps {
  handleSendMessage?: (key: string, text: string) => void;
  handleCancleSendMessage?: (key: string) => void;
}
