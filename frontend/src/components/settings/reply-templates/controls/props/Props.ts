import ReplyTemplate from "src/app/core/entities/reply-templates/ReplyTemplate";

export default interface Props {
  disabled: boolean;
  selected: ReplyTemplate | null;
  handleOnCancle(): void;
  handleSaveChangesAsync(title: string, content: string): Promise<void>;
}
