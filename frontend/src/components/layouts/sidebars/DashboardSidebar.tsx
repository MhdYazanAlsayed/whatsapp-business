import { Link } from "react-router-dom";
import SidebarMenu from "./menu/SidebarMenu";
import AccountActiveStatus from "./AccountActiveStatus";

const DashboardSidebar = () => {
  return (
    <aside className="dashboard-aside d-flex flex-column">
      <div className="d-flex flex-column justify-content-between py-3 gap-2 px-3">
        <SidebarMenu
          icon={
            <svg
              width="24"
              height="24"
              viewBox="0 0 25 26"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M22.9167 12.5486C22.9167 18.052 18.2523 22.5139 12.5 22.5139C11.8237 22.5149 11.1492 22.4523 10.4848 22.3276C10.0066 22.2377 9.76752 22.1928 9.60059 22.2183C9.43365 22.2438 9.1971 22.3696 8.72399 22.6212C7.3856 23.333 5.82499 23.5844 4.32411 23.3052C4.89456 22.6035 5.28416 21.7616 5.45606 20.8591C5.56023 20.3071 5.30212 19.7708 4.91555 19.3782C3.15976 17.5953 2.08337 15.1928 2.08337 12.5486C2.08337 7.04536 6.74773 2.58331 12.5 2.58331C18.2523 2.58331 22.9167 7.04536 22.9167 12.5486Z"
                stroke="#FAFAFA"
                strokeWidth="1.5"
                strokeLinejoin="round"
              />
              <path
                d="M12.4954 13H12.5047M16.6573 13H16.6667M8.33337 13H8.34272"
                stroke="#FAFAFA"
                strokeWidth="2"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
          }
          text="المحادثات"
        >
          <Link
            to="/conversations"
            className={`d-block rounded py-1 px-2 link`}
          >
            <div className="d-flex align-items-center gap-2">
              <svg
                width="24"
                height="24"
                viewBox="0 0 25 26"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M22.9167 12.5486C22.9167 18.052 18.2523 22.5139 12.5 22.5139C11.8237 22.5149 11.1492 22.4523 10.4848 22.3276C10.0066 22.2377 9.76752 22.1928 9.60059 22.2183C9.43365 22.2438 9.1971 22.3696 8.72399 22.6212C7.3856 23.333 5.82499 23.5844 4.32411 23.3052C4.89456 22.6035 5.28416 21.7616 5.45606 20.8591C5.56023 20.3071 5.30212 19.7708 4.91555 19.3782C3.15976 17.5953 2.08337 15.1928 2.08337 12.5486C2.08337 7.04536 6.74773 2.58331 12.5 2.58331C18.2523 2.58331 22.9167 7.04536 22.9167 12.5486Z"
                  stroke="#FAFAFA"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
                <path
                  d="M12.4954 13H12.5047M16.6573 13H16.6667M8.33337 13H8.34272"
                  stroke="#FAFAFA"
                  strokeWidth="2"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
              </svg>
              <span>محادثاتي</span>
            </div>
          </Link>

          <Link
            to="/bot/conversations"
            className="d-block rounded py-1 px-2 link"
          >
            <div className="d-flex align-items-center gap-2">
              <svg
                width="24"
                height="24"
                viewBox="0 0 28 28"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M23.3333 18.0834C24.622 18.0834 25.6666 17.0387 25.6666 15.75C25.6666 14.4613 24.622 13.4167 23.3333 13.4167"
                  stroke="#FAFAFA"
                  strokeWidth="1.5"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
                <path
                  d="M4.66663 18.0834C3.37793 18.0834 2.33329 17.0387 2.33329 15.75C2.33329 14.4613 3.37793 13.4167 4.66663 13.4167"
                  stroke="#FAFAFA"
                  strokeWidth="1.5"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
                <path
                  d="M19.8334 8.16669V4.66669"
                  stroke="#FAFAFA"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
                <path
                  d="M8.16663 8.16669V4.66669"
                  stroke="#FAFAFA"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
                <path
                  d="M19.8333 4.66665C19.189 4.66665 18.6667 4.14431 18.6667 3.49998C18.6667 2.85565 19.189 2.33331 19.8333 2.33331C20.4777 2.33331 21 2.85565 21 3.49998C21 4.14431 20.4777 4.66665 19.8333 4.66665Z"
                  stroke="#FAFAFA"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
                <path
                  d="M8.16671 4.66665C7.52237 4.66665 7.00004 4.14431 7.00004 3.49998C7.00004 2.85565 7.52237 2.33331 8.16671 2.33331C8.81104 2.33331 9.33337 2.85565 9.33337 3.49998C9.33337 4.14431 8.81104 4.66665 8.16671 4.66665Z"
                  stroke="#FAFAFA"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
                <path
                  d="M12.25 8.16669H15.75C19.0498 8.16669 20.6997 8.16669 21.7249 9.22716C22.75 10.2876 22.75 11.9944 22.75 15.4081C22.75 18.8217 22.75 20.5285 21.7249 21.589C20.6997 22.6495 19.0498 22.6495 15.75 22.6495H14.5538C13.6303 22.6495 13.3044 22.8402 12.668 23.5332C11.9642 24.2997 10.8744 25.3225 9.88843 25.5606C8.48703 25.8993 8.33012 25.4309 8.64278 24.0953C8.7318 23.7149 8.95393 23.1066 8.71967 22.7521C8.58842 22.5536 8.36978 22.5048 7.9324 22.4071C7.24243 22.253 6.67357 22.0011 6.27515 21.589C5.25 20.5285 5.25 18.8217 5.25 15.4081C5.25 11.9944 5.25 10.2876 6.27515 9.22716C7.3003 8.16669 8.9502 8.16669 12.25 8.16669Z"
                  stroke="#FAFAFA"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
                <path
                  d="M16.9166 17.5C16.2515 18.2084 15.1926 18.6667 14 18.6667C12.8073 18.6667 11.7484 18.2084 11.0833 17.5"
                  stroke="#FAFAFA"
                  strokeWidth="1.5"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
                <path
                  d="M17.4906 12.8333H17.5"
                  stroke="#FAFAFA"
                  strokeWidth="1.8"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
                <path
                  d="M10.4906 12.8333H10.5"
                  stroke="#FAFAFA"
                  strokeWidth="1.8"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                />
              </svg>
              <span>محادثات البوت</span>
            </div>
          </Link>

          <Link
            to="/unassigned/conversations/customer-service"
            className="d-block rounded py-1 px-2 link"
          >
            <div className="d-flex align-items-center gap-2">
              <svg
                width="24"
                height="24"
                viewBox="0 0 31 28"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M30 7.63246C30 11.1986 26.9775 14.09 23.25 14.09C22.8117 14.0906 22.3747 14.0501 21.9441 13.9692C21.6342 13.911 21.4792 13.882 21.3711 13.8985C21.2629 13.9149 21.1096 13.9965 20.803 14.1596C19.9357 14.6207 18.9245 14.7837 17.9519 14.6028C18.3216 14.1481 18.574 13.6025 18.6854 13.0177C18.7529 12.66 18.5857 12.3125 18.3352 12.0581C17.1974 10.9027 16.5 9.34586 16.5 7.63246C16.5 4.06633 19.5224 1.17493 23.25 1.17493C26.9775 1.17493 30 4.06633 30 7.63246Z"
                  stroke="#FAFAFA"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
                <path
                  d="M19.7145 7.92493H19.7254M23.7645 7.92493H23.7754"
                  stroke="#FAFAFA"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
                <path
                  d="M8.92755 26.8251H5.16953C4.73309 26.8251 4.29478 26.7637 3.89391 26.5914C2.58899 26.0308 1.92692 25.2904 1.61838 24.8272C1.44281 24.5639 1.46783 24.2281 1.6583 23.9751C3.17017 21.967 6.68192 20.7542 8.93398 20.754C11.186 20.7542 14.6914 21.967 16.2033 23.9751C16.3937 24.2281 16.4187 24.5639 16.2432 24.8272C15.9346 25.2904 15.2726 26.0308 13.9677 26.5914C13.5667 26.7637 13.1285 26.8251 12.692 26.8251H8.92755Z"
                  stroke="#FAFAFA"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
                <path
                  d="M12.6852 13.7151C12.6852 15.7824 11.0057 17.4583 8.9339 17.4583C6.86212 17.4583 5.18262 15.7824 5.18262 13.7151C5.18262 11.6479 6.86212 9.97205 8.9339 9.97205C11.0057 9.97205 12.6852 11.6479 12.6852 13.7151Z"
                  stroke="#FAFAFA"
                  strokeWidth="1.5"
                  strokeLinejoin="round"
                />
              </svg>
              <span>محادثات غير معينة</span>
            </div>
          </Link>
        </SidebarMenu>

        <Link to={"#"} className="d-block rounded py-1 link px-2">
          <div className="d-flex align-items-center gap-2 ">
            <svg
              width="24"
              height="24"
              viewBox="0 0 24 24"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M14.9263 2.91103L8.27352 6.10452C7.76151 6.35029 7.21443 6.41187 6.65675 6.28693C6.29177 6.20517 6.10926 6.16429 5.9623 6.14751C4.13743 5.93912 3 7.38342 3 9.04427V9.95573C3 11.6166 4.13743 13.0609 5.9623 12.8525C6.10926 12.8357 6.29178 12.7948 6.65675 12.7131C7.21443 12.5881 7.76151 12.6497 8.27352 12.8955L14.9263 16.089C16.4534 16.8221 17.217 17.1886 18.0684 16.9029C18.9197 16.6172 19.2119 16.0041 19.7964 14.778C21.4012 11.4112 21.4012 7.58885 19.7964 4.22196C19.2119 2.99586 18.9197 2.38281 18.0684 2.0971C17.217 1.8114 16.4534 2.17794 14.9263 2.91103Z"
                stroke="#FAFAFA"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
              <path
                d="M11.4581 20.7709L9.96674 22C6.60515 19.3339 7.01583 18.0625 7.01583 13H8.14966C8.60978 15.8609 9.69512 17.216 11.1927 18.197C12.1152 18.8012 12.3054 20.0725 11.4581 20.7709Z"
                stroke="#FAFAFA"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
              <path
                d="M7.5 12.5V6.5"
                stroke="#FAFAFA"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
            <span>الحملات التسويقية</span>
          </div>
        </Link>

        <Link to={"#"} className="d-block rounded py-1 link px-2">
          <div className="d-flex align-items-center gap-2 ">
            <svg
              width="24"
              height="24"
              viewBox="0 0 24 24"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M21 21H10C6.70017 21 5.05025 21 4.02513 19.9749C3 18.9497 3 17.2998 3 14V3"
                stroke="#FAFAFA"
                strokeWidth="1.5"
                strokeLinecap="round"
              />
              <path
                d="M7 4H8"
                stroke="#FAFAFA"
                strokeWidth="1.5"
                strokeLinecap="round"
              />
              <path
                d="M7 7H11"
                stroke="#FAFAFA"
                strokeWidth="1.5"
                strokeLinecap="round"
              />
              <path
                d="M5 20C6.07093 18.053 7.52279 13.0189 10.3063 13.0189C12.2301 13.0189 12.7283 15.4717 14.6136 15.4717C17.8572 15.4717 17.387 10 21 10"
                stroke="#FAFAFA"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
            <span>التقارير</span>
          </div>
        </Link>

        <Link
          to={"/settings/templates"}
          className="d-block rounded py-1 link px-2"
        >
          <div className="d-flex align-items-center gap-2 ">
            <svg
              width="24"
              height="24"
              viewBox="0 0 24 24"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M21 21H10C6.70017 21 5.05025 21 4.02513 19.9749C3 18.9497 3 17.2998 3 14V3"
                stroke="#FAFAFA"
                strokeWidth="1.5"
                strokeLinecap="round"
              />
              <path
                d="M7 4H8"
                stroke="#FAFAFA"
                strokeWidth="1.5"
                strokeLinecap="round"
              />
              <path
                d="M7 7H11"
                stroke="#FAFAFA"
                strokeWidth="1.5"
                strokeLinecap="round"
              />
              <path
                d="M5 20C6.07093 18.053 7.52279 13.0189 10.3063 13.0189C12.2301 13.0189 12.7283 15.4717 14.6136 15.4717C17.8572 15.4717 17.387 10 21 10"
                stroke="#FAFAFA"
                strokeWidth="1.5"
                strokeLinecap="round"
                strokeLinejoin="round"
              />
            </svg>
            <span>القوالب</span>
          </div>
        </Link>
      </div>
      <div className="px-3">
        <AccountActiveStatus />
      </div>
    </aside>
  );
};

export default DashboardSidebar;
