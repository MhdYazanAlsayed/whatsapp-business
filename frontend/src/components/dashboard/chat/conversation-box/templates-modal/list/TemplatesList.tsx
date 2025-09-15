import Props from "./props/Props";

const TemplatesList = ({
  templates,
  selected,
  handleGetDetailsAsync,
}: Props) => {
  return (
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
        <div className="responses-container">
          <div className="px-2 overflow-auto h-100p">
            {templates.length === 0 ? (
              <div
                className="d-flex align-items-center justify-content-center"
                style={{ height: "100%" }}
              >
                <div className="text-center text-muted">لا توجد بيانات</div>
              </div>
            ) : (
              templates.map((x, index) => (
                <div
                  className={`response cursor-pointer ${
                    x.id.value === selected?.id.value ? "selected" : ""
                  }`}
                  key={index}
                  onClick={() => handleGetDetailsAsync(x)}
                >
                  <div>
                    <p className="small mb-1 fw-semibold">{x.name}</p>
                    <p className="small">{x.category}</p>
                  </div>
                </div>
              ))
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default TemplatesList;
