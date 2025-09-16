import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import WorkGroup from "src/app/core/entities/work-groups/WorkGroup";
import App from "src/app/core/helpers/app_helpers/App";
import GetWorkGroupsQuery from "src/app/features/workgroups/queries/get-workgroups/GetWorkGroupsQuery";
const WorkGroups = () => {
  const [workGroups, setWorkGroups] = useState<WorkGroup[]>([]);

  useEffect(() => {
    handleGetWorkGroupsAsync();
  }, []);

  const handleGetWorkGroupsAsync = async () => {
    const response = await App.features.executeAsync(new GetWorkGroupsQuery());

    setWorkGroups(response);
  };

  return (
    <div className="work-groups overflow-hidden">
      <div className="overflow-auto">
        <Link
          to={"/unassigned/conversations/customer-service"}
          className="group active"
        >
          <svg
            width="22"
            height="22"
            viewBox="0 0 22 22"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M18.6771 16.25C19.3328 16.25 19.8543 15.8374 20.3225 15.2605C21.281 14.0795 19.7073 13.1357 19.107 12.6735C18.4969 12.2036 17.8156 11.9375 17.1249 11.875M16.2499 10.125C17.458 10.125 18.4374 9.14562 18.4374 7.9375C18.4374 6.72938 17.458 5.75 16.2499 5.75"
              stroke="#535766"
              strokeWidth="1.5"
              strokeLinecap="round"
            />
            <path
              d="M3.32272 16.25C2.66707 16.25 2.14559 15.8374 1.67735 15.2605C0.71883 14.0795 2.29259 13.1357 2.89281 12.6735C3.50298 12.2036 4.18426 11.9375 4.875 11.875M5.3125 10.125C4.10438 10.125 3.125 9.14562 3.125 7.9375C3.125 6.72938 4.10438 5.75 5.3125 5.75"
              stroke="#535766"
              strokeWidth="1.5"
              strokeLinecap="round"
            />
            <path
              d="M7.57321 13.7223C6.67915 14.2751 4.33499 15.404 5.76274 16.8165C6.46019 17.5065 7.23696 18 8.21355 18H13.7862C14.7628 18 15.5396 17.5065 16.237 16.8165C17.6648 15.404 15.3206 14.2751 14.4266 13.7223C12.33 12.4259 9.66972 12.4259 7.57321 13.7223Z"
              stroke="#535766"
              strokeWidth="1.5"
              strokeLinecap="round"
              strokeLinejoin="round"
            />
            <path
              d="M14.0624 7.0625C14.0624 8.75387 12.6913 10.125 10.9999 10.125C9.30852 10.125 7.93738 8.75387 7.93738 7.0625C7.93738 5.37112 9.30852 4 10.9999 4C12.6913 4 14.0624 5.37112 14.0624 7.0625Z"
              stroke="#535766"
              strokeWidth="1.5"
            />
          </svg>

          <small>قسم خدمة العملاء</small>
        </Link>

        {workGroups.map((x, index) => (
          <Link
            to={`/unassigned/conversations/${x.id.value}`}
            className="group active"
            key={index}
          >
            <svg
              width="22"
              height="22"
              viewBox="0 0 22 22"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M18.6771 16.25C19.3328 16.25 19.8543 15.8374 20.3225 15.2605C21.281 14.0795 19.7073 13.1357 19.107 12.6735C18.4969 12.2036 17.8156 11.9375 17.1249 11.875M16.2499 10.125C17.458 10.125 18.4374 9.14562 18.4374 7.9375C18.4374 6.72938 17.458 5.75 16.2499 5.75"
                stroke="#535766"
                strokeWidth="1.5"
                strokeLinecap="round"
              />
              <path
                d="M3.32272 16.25C2.66707 16.25 2.14559 15.8374 1.67735 15.2605C0.71883 14.0795 2.29259 13.1357 2.89281 12.6735C3.50298 12.2036 4.18426 11.9375 4.875 11.875M5.3125 10.125C4.10438 10.125 3.125 9.14562 3.125 7.9375C3.125 6.72938 4.10438 5.75 5.3125 5.75"
                stroke="#535766"
                strokeWidth="1.5"
                strokeLinecap="round"
              />
              <path
                d="M7.57321 13.7223C6.67915 14.2751 4.33499 15.404 5.76274 16.8165C6.46019 17.5065 7.23696 18 8.21355 18H13.7862C14.7628 18 15.5396 17.5065 16.237 16.8165C17.6648 15.404 15.3206 14.2751 14.4266 13.7223C12.33 12.4259 9.66972 12.4259 7.57321 13.7223Z"
                stroke="#535766"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
              <path
                d="M14.0624 7.0625C14.0624 8.75387 12.6913 10.125 10.9999 10.125C9.30852 10.125 7.93738 8.75387 7.93738 7.0625C7.93738 5.37112 9.30852 4 10.9999 4C12.6913 4 14.0624 5.37112 14.0624 7.0625Z"
                stroke="#535766"
                strokeWidth="1.5"
              />
            </svg>

            <small>{x.name}</small>
          </Link>
        ))}
      </div>
    </div>
  );
};

export default WorkGroups;
