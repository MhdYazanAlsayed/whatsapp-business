import { useState } from "react";
import { Fragment } from "react/jsx-runtime";
import { TemplateComponentFormat } from "src/app/core/entities/templates/template-components/enums/TemplateComponentFormat";
import Props from "./Props";

const HeaderFileEditor = ({
  direction,
  headerComponent,
  setHeaderComponent,
}: Props) => {
  const [file, setFile] = useState<File | null>(null);

  const handleOnChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFile(e.target.files?.[0] || null);

    setHeaderComponent((x) => ({
      ...x,
      example: {
        ...x.example,
        headerHandle: file === null ? undefined : file,
      },
    }));
  };

  return (
    <Fragment>
      <div className="col-md-12 mb-3">
        <div className="form-group">
          <label htmlFor="text" className="mb-1 text-muted fw-semibold">
            {headerComponent.format == TemplateComponentFormat.Document
              ? "الملف"
              : headerComponent.format == TemplateComponentFormat.Image
              ? "الصورة"
              : headerComponent.format == TemplateComponentFormat.Video
              ? "الفيديو"
              : ""}

            {" ( كمثال )"}
            <span className="text-danger">*</span>
          </label>

          <input
            type="file"
            className="form-control"
            dir={direction}
            accept={
              headerComponent.format == TemplateComponentFormat.Image
                ? "image/*"
                : headerComponent.format == TemplateComponentFormat.Video
                ? "video/*"
                : headerComponent.format == TemplateComponentFormat.Document
                ? "application/pdf"
                : ""
            }
            value={file?.name}
            onChange={handleOnChange}
          />
        </div>
      </div>
    </Fragment>
  );
};

export default HeaderFileEditor;
