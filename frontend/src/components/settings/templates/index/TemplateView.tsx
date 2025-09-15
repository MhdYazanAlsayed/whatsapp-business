import { Fragment } from "react/jsx-runtime";
import { TemplateLanguage } from "src/app/core/entities/templates/enums/TemplateLanguage";
import Template from "src/app/core/entities/templates/Template";
import { TemplateComponentFormat } from "src/app/core/entities/templates/template-components/enums/TemplateComponentFormat";
import { TemplateComponentType } from "src/app/core/entities/templates/template-components/enums/TemplateComponentType";

const TemplateView = ({ template }: { template: Template }) => {
  const header = template.components.find(
    (x) => x.type == TemplateComponentType.Header
  );

  const body = template.components.find(
    (x) => x.type == TemplateComponentType.Body
  );

  const footer = template.components.find(
    (x) => x.type == TemplateComponentType.Footer
  );

  const buttons = template.buttons;

  console.log(buttons);
  return (
    <Fragment>
      <div
        className="template rounded p-2 mb-1"
        dir={template.language == TemplateLanguage.Arabic ? "rtl" : "ltr"}
      >
        {header &&
          (header.format == TemplateComponentFormat.Document ? (
            <div className="header mb-2">
              <div className="d-flex align-items-center gap-2">
                <i className="fa-solid fa-file-document"></i>
                <span>
                  {template.language == TemplateLanguage.Arabic
                    ? "ملف أو مستند"
                    : "Document or file"}
                </span>
              </div>
            </div>
          ) : header.format == TemplateComponentFormat.Image ? (
            <div className="header mb-2">
              <div className="d-flex align-items-center gap-2">
                <i className="fa-solid fa-file-image"></i>
                <span>
                  {template.language == TemplateLanguage.Arabic
                    ? "صورة"
                    : "Image"}
                </span>
              </div>
            </div>
          ) : header.format == TemplateComponentFormat.Video ? (
            <div className="header mb-2">
              <div className="d-flex align-items-center gap-2">
                <i className="fa-solid fa-file-video"></i>
                <span>
                  {template.language == TemplateLanguage.Arabic
                    ? "فيديو"
                    : "Video"}
                </span>
              </div>
            </div>
          ) : header.format == TemplateComponentFormat.Text ? (
            <div className="no-style mb-2">
              <span>{header.text}</span>
            </div>
          ) : (
            <></>
          ))}

        {body && (
          <div className="body">
            <div className="d-flex align-items-center gap-2">
              <span>
                {body.text ? (
                  body.text
                ) : (
                  <small className="text-muted text-center">
                    {template.language == TemplateLanguage.Arabic
                      ? "لا يوجد نص"
                      : "No text"}
                  </small>
                )}
              </span>
            </div>
          </div>
        )}

        {footer && (
          <div className="footer mt-2">
            <div className="d-flex align-items-center gap-2 text-muted">
              <small>{footer.text}</small>
            </div>
          </div>
        )}
      </div>

      {buttons.length === 0 ? null : buttons.length === 1 ? (
        <button className="btn btn-default bg-white text-blue w-100">
          {buttons[0].text}
        </button>
      ) : buttons.length === 2 ? (
        <div className="d-flex align-items-center gap-2">
          <button className="btn btn-default bg-white flex-grow-1 text-blue">
            {buttons[0].text}
          </button>
          <button className="btn btn-default bg-white flex-grow-1 text-blue">
            {buttons[1].text}
          </button>
        </div>
      ) : buttons.length === 3 ? (
        <div className="">
          <div className="d-flex align-items-center gap-2 mb-1">
            <button className="btn btn-default bg-white flex-grow-1 text-blue">
              {buttons[0].text}
            </button>
            <button className="btn btn-default bg-white flex-grow-1 text-blue">
              {buttons[1].text}
            </button>
          </div>

          <button className="btn btn-default bg-white w-100 text-blue">
            {buttons[2].text}
          </button>
        </div>
      ) : null}
    </Fragment>
  );
};

export default TemplateView;
