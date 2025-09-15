import { toast } from "react-toastify";
import { TemplateButtonType } from "src/app/core/entities/templates/enums/TemplateButtonType";
import { TemplateButtonPayload } from "src/app/features/templates/commands/create/CreateTemplatePayload";

const Buttons = ({
  buttons,
  setButtons,
}: {
  buttons: TemplateButtonPayload[];
  setButtons: (buttons: TemplateButtonPayload[]) => void;
}) => {
  const handleAddButton = (
    e: React.MouseEvent<HTMLButtonElement>,
    type: TemplateButtonType
  ) => {
    e.preventDefault();

    if (buttons.length >= 3) {
      toast.error("لا يمكن إضافة أكثر من 3 أزرار");
      return;
    }

    setButtons([...buttons, { type: type, text: "" }]);
  };

  const handleRemoveButton = (
    e: React.MouseEvent<HTMLButtonElement>,
    index: number
  ) => {
    e.preventDefault();

    setButtons(buttons.filter((_, i) => i !== index));
  };

  const handleChangeButton = (
    index: number,
    value: string,
    url?: string,
    phoneNumber?: string
  ) => {
    setButtons(
      buttons.map((button, i) =>
        i === index
          ? { ...button, text: value, url: url, phoneNumber: phoneNumber }
          : button
      )
    );
  };

  return (
    <div className="mb-3">
      <h5 className="py-3 mb-3 text-blue fw-semibold">الأزرار</h5>

      {buttons.map((button, index) => (
        <div key={index} className="col-md-12 mb-3">
          <label
            htmlFor={`button-${index}`}
            className="mb-1 text-muted fw-semibold"
          >
            الزر {index + 1}
          </label>
          {button.type === TemplateButtonType.QuickReply && (
            <div className="">
              <input
                type="text"
                value={button.text}
                className="form-control mb-2"
                placeholder="الرد سريع"
                onChange={(e) => handleChangeButton(index, e.target.value)}
              />

              <button
                className="btn btn-danger"
                onClick={(e) => handleRemoveButton(e, index)}
              >
                حذف
              </button>
            </div>
          )}
          {button.type === TemplateButtonType.Url && (
            <div className="">
              <input
                type="text"
                value={button.text}
                className="form-control mb-2"
                placeholder="عنوان الزر"
                onChange={(e) => handleChangeButton(index, e.target.value)}
              />

              <input
                type="url"
                value={button.url}
                className="form-control mb-2"
                placeholder="الرابط"
                onChange={(e) =>
                  handleChangeButton(
                    index,
                    button.text ?? "",
                    e.target.value,
                    button.phoneNumber
                  )
                }
              />

              <button
                className="btn btn-danger"
                onClick={(e) => handleRemoveButton(e, index)}
              >
                حذف
              </button>
            </div>
          )}

          {button.type === TemplateButtonType.PhoneNumber && (
            <div className="">
              <input
                type="text"
                value={button.text}
                className="form-control mb-2"
                placeholder="عنوان الزر"
                onChange={(e) => handleChangeButton(index, e.target.value)}
              />

              <input
                type="tel"
                value={button.phoneNumber}
                className="form-control mb-2"
                placeholder="رقم الهاتف"
                onChange={(e) =>
                  handleChangeButton(
                    index,
                    button.text ?? "",
                    button.url,
                    e.target.value
                  )
                }
              />

              <button
                className="btn btn-danger"
                onClick={(e) => handleRemoveButton(e, index)}
              >
                حذف
              </button>
            </div>
          )}
        </div>
      ))}

      <div className="col-md-12">
        <div className="d-flex align-items-center gap-2">
          {buttons.some(
            (button) => button.type === TemplateButtonType.Url
          ) ? null : buttons.some(
              (button) => button.type === TemplateButtonType.PhoneNumber
            ) ? null : buttons.some(
              (button) => button.type === TemplateButtonType.QuickReply
            ) ? (
            <button
              className="custom-btn rounded primary"
              onClick={(e) => handleAddButton(e, TemplateButtonType.QuickReply)}
            >
              اضافة رز للرد سريع
            </button>
          ) : (
            <>
              <button
                className="custom-btn rounded primary"
                onClick={(e) =>
                  handleAddButton(e, TemplateButtonType.QuickReply)
                }
              >
                اضافة رز للرد سريع
              </button>

              <button
                className="custom-btn rounded primary"
                onClick={(e) => handleAddButton(e, TemplateButtonType.Url)}
              >
                اضافة رز رابط
              </button>

              <button
                className="custom-btn rounded primary"
                onClick={(e) =>
                  handleAddButton(e, TemplateButtonType.PhoneNumber)
                }
              >
                اضافة رز رقم الهاتف
              </button>
            </>
          )}
        </div>
      </div>
    </div>
  );
};

export default Buttons;
