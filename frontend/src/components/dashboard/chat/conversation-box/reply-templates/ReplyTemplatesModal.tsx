import { FormEvent, useEffect, useState } from "react";
import Props from "./props/Props";
import TadawiModal from "src/components/shared/modal/TadawiModal";
import ReplyTemplate from "src/app/core/entities/reply-templates/ReplyTemplate";
import MediatR from "src/app/core/helpers/mediatR/MediatR";
import GetReplyTemplatePaginationQuery from "src/app/features/reply-templates/queries/get-pagination/GetReplyTemplatePaginationQuery";
import { useParams } from "react-router-dom";
import DependenciesInjector from "src/app/core/util/DependenciesInjector";
import { toast } from "react-toastify";

const pagination = { currentPage: 1, pages: 1 };
const ReplyTemplatesModal = ({
  open,
  setOpen,
  handleSendMessage,
  handleCancleSendMessage,
}: Props) => {
  const [replyTemplates, setReplyTemplates] = useState<ReplyTemplate[]>([]);
  const [selected, setSelected] = useState<ReplyTemplate | null>(null);
  const [formData, setFormData] = useState("");

  const { id } = useParams();

  useEffect(() => {
    if (open) {
      handleGetReplyTemplatesAsync();
      setSelected(null);
      setFormData("");
    }
  }, [open]);

  const handleGetReplyTemplatesAsync = async () => {
    const result = await MediatR.features.executeAsync(
      new GetReplyTemplatePaginationQuery({
        page: pagination.currentPage,
      })
    );

    setReplyTemplates(result.data);
    pagination.pages = result.pages;
  };

  const handleSelectReplyTemplate = (data: ReplyTemplate) => {
    setSelected(data);
    setFormData(data.content);
  };

  // To send reply template
  const handleOnSubmitAsync = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!handleSendMessage || !handleCancleSendMessage || !id)
      throw new Error();

    const date = Date().toString();
    const primaryKey = "System_" + formData + "_" + date;

    handleSendMessage(primaryKey, formData);

    const response = await _conversationService.sendMessageAsync(id, formData);

    if (!response.succeeded) {
      // Remove the message
      toast.error(`فشل ارسال الرسالة '${formData}'`);
      handleCancleSendMessage(primaryKey);
    }

    setSelected(null);
    setOpen(false);
  };

  const handleOnClose = (e: FormEvent<HTMLButtonElement>) => {
    e.preventDefault();

    setSelected(null);
    setOpen(false);
  };

  return (
    <TadawiModal
      open={open}
      setOpen={setOpen}
      width={"1000px"}
      className="ready-responses-modal"
    >
      <p className="fw-semibold mb-3">اختر ردا جاهزا من هنا</p>
      <div className="tabs">
        <button className="active">الردود الجاهزة</button>
        {/* <button>القوالب</button> */}
      </div>
      <div className="row">
        <div className="col-6">
          <div className="responses">
            <label className="custom-input search-box">
              <svg
                width="17"
                height="16"
                viewBox="0 0 18 19"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M13.125 13.625L16.5 17"
                  stroke="#6E6E6E"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
                <path
                  d="M15 8.75C15 5.02208 11.978 2 8.25 2C4.52208 2 1.5 5.02208 1.5 8.75C1.5 12.478 4.52208 15.5 8.25 15.5C11.978 15.5 15 12.478 15 8.75Z"
                  stroke="#6E6E6E"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
              </svg>

              <input className="" type="text" placeholder="ابحث..." />
            </label>
            {/* List */}
            <div className="responses-container">
              <div className="px-2 overflow-auto h-100p">
                {replyTemplates.map((x, index) => (
                  <div
                    className={`response cursor-pointer ${
                      x.id === selected?.id ? "selected" : null
                    }`}
                    key={index}
                    onClick={() => handleSelectReplyTemplate(x)}
                  >
                    <div>
                      <p className="small mb-1 fw-semibold">{x.title}</p>
                      <p className="small">{x.content}</p>
                    </div>
                  </div>
                ))}
              </div>
            </div>
          </div>
        </div>
        {/* Controls */}
        <div className="col-6">
          {selected && (
            <form onSubmit={handleOnSubmitAsync} className="selected-response">
              <div className="response-content pb-3">
                <textarea
                  name=""
                  id=""
                  value={formData}
                  onChange={(x) => setFormData(x.currentTarget.value)}
                ></textarea>
              </div>

              <div className="buttons">
                <button
                  className="custom-btn primary"
                  disabled={formData.trim() === ""}
                >
                  ارسال
                </button>
                <button
                  className="custom-btn secondary"
                  onClick={handleOnClose}
                >
                  اغلاق
                </button>
              </div>
            </form>
          )}
        </div>
      </div>
    </TadawiModal>
  );
};

export default ReplyTemplatesModal;
const _conversationService = DependenciesInjector.services.conversationSerivce;
