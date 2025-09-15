export interface ControlsProps {
  id?: string;
  disabled?: boolean;
  handleSendMessage?: (key: string, text: string) => void;
  handleCancleSendMessage?: (key: string) => void;
}
