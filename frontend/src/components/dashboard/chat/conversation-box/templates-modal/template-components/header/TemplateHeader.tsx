import { TemplateComponentFormat } from "src/app/core/entities/templates/template-components/enums/TemplateComponentFormat";
import Props from "./props/Props";
import Caption from "./Caption";

const TemplateHeader = ({
  data,
  isRTL,
  fileName,
  handleAddHeaderParameterValue,
}: Props) => {
  if (!data) return null;

  const { format, text } = data;
  const direction = isRTL ? "rtl" : "ltr";

  const handleOnChange = (x: React.ChangeEvent<HTMLInputElement>) => {
    handleAddHeaderParameterValue(x.currentTarget.files?.item(0) ?? null);
  };

  if (format == null || format === TemplateComponentFormat.Text)
    return <div dir={direction}>{text}</div>;

  if (format === TemplateComponentFormat.Image)
    return (
      <div className="template-image" dir={direction}>
        <input type="file" accept="image/*" onChange={handleOnChange} />
        <small className="d-flex align-items-center gap-2">
          <Caption fileName={fileName} text="ارفع صورة" />

          <i className="fa-solid fa-upload"></i>
        </small>
      </div>
    );

  if (format === TemplateComponentFormat.Video)
    return (
      <div className="template-video" dir={direction}>
        <input type="file" accept="video/*" onChange={handleOnChange} />
        <small className="d-flex align-items-center gap-2">
          <Caption fileName={fileName} text="ارفع فيديو" />
          <i className="fa-solid fa-upload"></i>
        </small>
      </div>
    );

  if (format === TemplateComponentFormat.Document)
    return (
      <div className="template-video" dir={direction}>
        <input
          accept="application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/pdf"
          type="file"
          onChange={handleOnChange}
        />
        <small className="d-flex align-items-center gap-2">
          <Caption fileName={fileName} text="ارفع مستند" />
          <i className="fa-solid fa-upload"></i>
        </small>
      </div>
    );

  throw new Error("Not implemented template format type");
};

export default TemplateHeader;
