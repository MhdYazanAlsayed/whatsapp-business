import CaptionProps from "./props/CaptionProps";

const Caption = ({ fileName, text }: CaptionProps) => {
  if (fileName.length === 0) return <span>{text}</span>;

  return <span>{fileName}</span>;
};

export default Caption;
