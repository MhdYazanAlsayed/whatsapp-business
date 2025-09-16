import { useEffect, useState } from "react";
import { Fragment } from "react/jsx-runtime";
import ReplyTemplate from "src/app/core/entities/reply-templates/ReplyTemplate";
import App from "src/app/core/helpers/app_helpers/App";
import GetReplyTemplatePaginationQuery from "src/app/features/reply-templates/queries/get-pagination/GetReplyTemplatePaginationQuery";
import ReplyItem from "./reply-item/ReplyItem";
import Controls from "./controls/Controls";
import UpdateReplyTemplateCommand from "src/app/features/reply-templates/commands/update/UpdateReplyTemplateCommand";
import CreateReplyTemplateCommand from "src/app/features/reply-templates/commands/create/CreateReplyTemplateCommand";

const pagination = { currentPage: 1, pages: 1 };
const Index = () => {
  const [replyTemplates, setReplyTemplates] = useState<ReplyTemplate[]>([]);
  const [controlsDisable, setControlsDisable] = useState(true);
  const [selected, setSelected] = useState<ReplyTemplate | null>(null);

  useEffect(() => {
    handleGetReplyTemplatesAsync();
  }, []);

  const handleGetReplyTemplatesAsync = async () => {
    const result = await App.features.executeAsync(
      new GetReplyTemplatePaginationQuery({
        page: pagination.currentPage,
      })
    );

    setReplyTemplates(result.data);
    pagination.pages = result.pages;
  };

  const handleSwitchToAddForm = () => {
    setSelected(null);
    setControlsDisable(false);
  };

  const handleSwitchToEditForm = (data: ReplyTemplate) => {
    setSelected(data);
  };

  const handleSaveChangesAsync = async (title: string, content: string) => {
    if (selected != null) {
      handleUpdateAsync(title, content);
      return;
    }

    handleCreateAsync(title, content);
  };

  const handleOnCancle = () => {
    setSelected(null);
    setControlsDisable(true);
  };

  const handleUpdateAsync = async (title: string, content: string) => {
    const result = await App.features.executeAsync(
      new UpdateReplyTemplateCommand({
        content: content,
        title: title,
        id: selected!.id,
      })
    );

    setReplyTemplates((prev) => {
      const index = prev.findIndex((x) => x.id.value === result.id.value);
      if (index === -1) throw new Error();

      prev[index] = result;

      return [...prev];
    });
  };

  const handleCreateAsync = async (title: string, content: string) => {
    const result = await App.features.executeAsync(
      new CreateReplyTemplateCommand({
        content: content,
        title: title,
      })
    );

    const newReplyTemplate = { ...result };

    setReplyTemplates((prev) => {
      prev.push(newReplyTemplate);
      return [...prev];
    });
    setSelected(newReplyTemplate);
  };

  return (
    <Fragment>
      <div className="row h-100p">
        <div className="col-5 d-flex flex-column h-100p">
          <button
            onClick={handleSwitchToAddForm}
            className="custom-btn primary d-flex align-items-center justify-content-center gap-2 rounded mb-2"
          >
            <svg
              width="14"
              height="14"
              viewBox="0 0 16 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M8 1V15M15 8H1"
                stroke="#FAFAFA"
                strokeWidth="2"
                strokeLinecap="round"
                strokeLinejoin="round"
              ></path>
            </svg>
            <span>إنشاء رد جديد</span>
          </button>
          {/* List */}
          <div className="responses-container flex-fill">
            <div className="overflow-auto h-100p">
              {/* List */}
              {replyTemplates
                .sort(
                  (a, b) =>
                    new Date(b.createdAt).getTime() -
                    new Date(a.createdAt).getTime()
                )
                .map((x, index) => (
                  <ReplyItem
                    data={x}
                    key={index}
                    onClick={handleSwitchToEditForm}
                  />
                ))}
            </div>
          </div>
        </div>

        <Controls
          disabled={controlsDisable}
          selected={selected}
          handleOnCancle={handleOnCancle}
          handleSaveChangesAsync={handleSaveChangesAsync}
        />
      </div>
      {/* <div className="settings d-flex flex-column">
        <div className="work-groups mb-4">
          <div className="overflow-auto">
            <a href="./account-settings.html" className="group">
              <svg
                width="20"
                height="20"
                viewBox="0 0 19 20"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M5.20725 12.7569C4.08722 13.4238 1.15058 14.7856 2.93919 16.4896C3.81291 17.322 4.78602 17.9173 6.00944 17.9173H12.9905C14.214 17.9173 15.1871 17.322 16.0608 16.4896C17.8494 14.7856 14.9128 13.4238 13.7927 12.7569C11.1663 11.193 7.83369 11.193 5.20725 12.7569Z"
                  stroke="#535766"
                  strokeWidth="1.5"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
                <path
                  d="M13.0625 5.64648C13.0625 7.614 11.4675 9.20898 9.5 9.20898C7.53249 9.20898 5.9375 7.614 5.9375 5.64648C5.9375 3.67897 7.53249 2.08398 9.5 2.08398C11.4675 2.08398 13.0625 3.67897 13.0625 5.64648Z"
                  stroke="#535766"
                  strokeWidth="1.5"
                />
              </svg>
              <small>الملف الشخصي</small>
            </a>
            <a href="./general-settings.html" className="group">
              <svg
                width="20"
                height="20"
                viewBox="0 0 20 20"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M12.9167 9.58268C12.9167 11.1935 11.6108 12.4993 10 12.4993C8.38916 12.4993 7.08333 11.1935 7.08333 9.58268C7.08333 7.97185 8.38916 6.66602 10 6.66602C11.6108 6.66602 12.9167 7.97185 12.9167 9.58268Z"
                  stroke="#535766"
                  strokeWidth="1.5"
                />
                <path
                  d="M17.5 11.3323C17.7629 11.2605 18.0419 11.2218 18.3333 11.2218V7.94372C15.9528 7.94372 14.4048 5.35802 15.61 3.30511L12.7233 1.6661C11.5032 3.74439 8.49817 3.74431 7.27803 1.66602L4.39128 3.30502C5.59655 5.35797 4.04728 7.94372 1.66667 7.94372V11.2218C4.04724 11.2218 5.59521 13.8074 4.38996 15.8603L7.27671 17.4993C7.88695 16.4599 8.94342 15.9401 10 15.9398"
                  stroke="#535766"
                  strokeWidth="1.5"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
                <path
                  d="M15.4167 12.5L15.6316 13.0808C15.9134 13.8425 16.0543 14.2233 16.3322 14.5012C16.61 14.779 16.9908 14.9199 17.7525 15.2017L18.3333 15.4167L17.7525 15.6316C16.9908 15.9134 16.61 16.0543 16.3322 16.3322C16.0543 16.61 15.9134 16.9908 15.6316 17.7525L15.4167 18.3333L15.2017 17.7525C14.9199 16.9908 14.779 16.61 14.5012 16.3322C14.2233 16.0543 13.8425 15.9134 13.0808 15.6316L12.5 15.4167L13.0808 15.2017C13.8425 14.9199 14.2233 14.779 14.5012 14.5012C14.779 14.2233 14.9199 13.8425 15.2017 13.0808L15.4167 12.5Z"
                  stroke="#535766"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
              </svg>
              <small>عامة</small>
            </a>
            <a href="" className="group active">
              <svg
                width="20"
                height="20"
                viewBox="0 0 21 20"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M10.4963 10H10.5038M7.16667 10H7.17414"
                  stroke="#535766"
                  strokeWidth="1.5"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
                <path
                  d="M18.8333 9.63827C18.8333 14.0409 15.1018 17.6105 10.5 17.6105C9.95892 17.6113 9.41933 17.5612 8.88783 17.4614C8.50528 17.3895 8.31398 17.3536 8.18044 17.374C8.04689 17.3944 7.85765 17.4951 7.47916 17.6963C6.40845 18.2658 5.15996 18.4668 3.95926 18.2435C4.41562 17.6822 4.72729 17.0087 4.86482 16.2867C4.94815 15.845 4.74167 15.416 4.43241 15.1019C3.02778 13.6756 2.16667 11.7536 2.16667 9.63827C2.16667 5.23566 5.89815 1.66602 10.5 1.66602C11.0708 1.66602 11.6282 1.72093 12.1667 1.82553"
                  stroke="#535766"
                  strokeWidth="1.5"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
                <path
                  d="M17.8655 2.06338L18.4424 2.64036C18.9306 3.12851 18.9306 3.91997 18.4424 4.40812L15.4196 7.48784C15.1818 7.72563 14.8777 7.88593 14.5471 7.94767L12.6737 8.35434C12.3778 8.41859 12.1144 8.15595 12.1778 7.85998L12.5766 5.9972C12.6383 5.66663 12.7986 5.36247 13.0364 5.12468L16.0978 2.06338C16.5858 1.57523 17.3773 1.57523 17.8655 2.06338Z"
                  stroke="#535766"
                  strokeWidth="1.5"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
              </svg>

              <small>اعدادت المحادثة</small>
            </a>
            <a href="" className="group">
              <svg
                width="20"
                height="20"
                viewBox="0 0 20 20"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M10 18.3327C14.6023 18.3327 18.3333 14.6017 18.3333 9.99935C18.3333 5.39697 14.6023 1.66602 10 1.66602C5.39763 1.66602 1.66667 5.39697 1.66667 9.99935C1.66667 11.1484 1.89923 12.2432 2.31985 13.2391C2.55232 13.7894 2.66856 14.0647 2.68295 14.2727C2.69734 14.4807 2.63612 14.7094 2.51369 15.167L1.66667 18.3327L4.83231 17.4857C5.2899 17.3633 5.5187 17.302 5.72669 17.3164C5.93468 17.3308 6.20988 17.447 6.7603 17.6795C7.75621 18.1001 8.85092 18.3327 10 18.3327Z"
                  stroke="#535766"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
                <path
                  d="M7.15679 10.3151L7.88257 9.41365C8.18846 9.03373 8.56658 8.68007 8.59625 8.1742C8.60366 8.04643 8.51383 7.47279 8.33399 6.32553C8.26334 5.87466 7.84238 5.83398 7.47776 5.83398C7.00261 5.83398 6.76504 5.83398 6.52912 5.94174C6.23094 6.07794 5.92482 6.46091 5.85764 6.78176C5.80449 7.03562 5.84399 7.21054 5.92299 7.56039C6.25852 9.04632 7.04567 10.5138 8.26623 11.7344C9.48683 12.955 10.9543 13.7422 12.4402 14.0777C12.7901 14.1567 12.965 14.1962 13.2189 14.143C13.5397 14.0758 13.9227 13.7697 14.0589 13.4715C14.1667 13.2356 14.1667 12.9981 14.1667 12.5229C14.1667 12.1582 14.126 11.7373 13.6751 11.6667C12.5278 11.4868 11.9542 11.397 11.8264 11.4044C11.3206 11.4341 10.9669 11.8122 10.587 12.1181L9.68558 12.8438"
                  stroke="#535766"
                  strokeWidth="1.5"
                />
              </svg>

              <small>اعدادت الواتساب</small>
            </a>
          </div>
        </div>
        <section className="conversations-settings">
    
        </section>
      </div> */}
    </Fragment>
  );
};

export default Index;
