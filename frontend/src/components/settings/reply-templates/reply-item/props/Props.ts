import ReplyTemplate from "src/app/core/entities/reply-templates/ReplyTemplate";

export default interface Props {
  data: ReplyTemplate;
  onClick(data: ReplyTemplate): void;
}
