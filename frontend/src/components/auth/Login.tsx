import WhiteLogo from "src/assets/images/white-logo.svg";
import BlueRedLines from "src/assets/images/Blue-Red-Lines.png";
import Background from "src/assets/images/Background.png";
import DependenciesInjector from "src/app/core/util/DependenciesInjector";
import { Navigate } from "react-router-dom";
import { FormEvent } from "react";

const _authenticator = DependenciesInjector.services.authenticator;
const hostEnviroment = DependenciesInjector.services.hostEnviroment;

const Login = () => {
  if (_authenticator.isAuthenticated) return <Navigate to={"/"} />;

  const handleNavigateToIDPService = (e: FormEvent<HTMLDivElement>) => {
    e.preventDefault();

    window.location.href =
      hostEnviroment.identityProvider +
      `Account/Login?ClientId=F65BB554C6DFAEF1BF3EB1B2A28F7&ReturnUrl=${
        hostEnviroment.frontendUrl + "login/callback"
      }`;
  };

  return (
    <main className="w-100 h-100 position-relative d-flex align-items-center justify-content-center">
      <div
        className="bg-img"
        style={{ backgroundImage: `url(${Background})` }}
      ></div>
      <div className="login-form shadow">
        <div className="form-header">
          <img src={BlueRedLines} className="red-blue-lines" alt="" />
          <img src={WhiteLogo} className="white-logo" alt="" />
          <h2 className="fw-semibold m-0 text-white z-1">... صحتك أولويتنا</h2>
        </div>
        <div className="form-body">
          <p className="mb-4 text-muted">يجب عليك تسجيل الدخول اولا</p>
          <div
            onClick={handleNavigateToIDPService}
            className="text-center custom-btn primary rounded w-100 cursor-pointer"
          >
            تسجيل الدخول
          </div>
        </div>
        {/* <div className="form-body">
          <form onSubmit={handleOnSubmitAsync}>
            <div className="mb-3">
              <label className="fw-semibold text-muted mb-2">
                اسم المستخدم
              </label>
              <label className="custom-input">
                <svg
                  width="28"
                  height="20"
                  viewBox="0 0 34 26"
                  fill="none"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path
                    d="M17.296 12.5995C17.2644 12.5995 17.2433 12.5995 17.2117 12.5995C17.1589 12.589 17.0851 12.589 17.0218 12.5995C13.9637 12.5046 11.6543 10.1003 11.6543 7.13706C11.6543 4.12111 14.1113 1.66406 17.1273 1.66406C20.1432 1.66406 22.6003 4.12111 22.6003 7.13706C22.5898 10.1003 20.2698 12.5046 17.3277 12.5995C17.3171 12.5995 17.3066 12.5995 17.296 12.5995ZM17.1273 3.24585C14.9866 3.24585 13.2361 4.99637 13.2361 7.13706C13.2361 9.24612 14.8812 10.9439 16.9797 11.0177C17.0324 11.0072 17.18 11.0072 17.3171 11.0177C19.384 10.9228 21.008 9.22503 21.0185 7.13706C21.0185 4.99637 19.268 3.24585 17.1273 3.24585Z"
                    fill="#212529bf"
                  />
                  <path
                    d="M17.307 24.1249C15.2401 24.1249 13.1627 23.5976 11.5915 22.5431C10.1257 21.5729 9.32422 20.2442 9.32422 18.7995C9.32422 17.3548 10.1257 16.0155 11.5915 15.0348C14.755 12.9363 19.88 12.9363 23.0225 15.0348C24.4778 16.005 25.2898 17.3337 25.2898 18.7784C25.2898 20.2231 24.4883 21.5623 23.0225 22.5431C21.4407 23.5976 19.3739 24.1249 17.307 24.1249ZM12.4667 16.3635C11.4544 17.0384 10.906 17.9031 10.906 18.81C10.906 19.7064 11.4649 20.5711 12.4667 21.2354C15.0925 22.9965 19.5215 22.9965 22.1473 21.2354C23.1596 20.5605 23.708 19.6958 23.708 18.7889C23.708 17.8926 23.1491 17.0279 22.1473 16.3635C19.5215 14.613 15.0925 14.613 12.4667 16.3635Z"
                    fill="#212529bf"
                  />
                </svg>
                <input
                  type="text"
                  id="user-name"
                  placeholder="ادخل اسم المستخدم"
                  value={formData.userName}
                  onChange={(x) =>
                    handleOnChange("userName", x.currentTarget.value)
                  }
                />
              </label>
            </div>
            <div className="mb-3">
              <label className="fw-semibold text-muted mb-2">كلمة المرور</label>
              <label className="custom-input">
                <svg
                  width="28"
                  height="20"
                  viewBox="0 0 22 22"
                  fill="none"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path
                    d="M3.79122 17.27C3.98799 18.7316 5.1985 19.8765 6.67159 19.9442C7.91112 20.0012 9.17027 20.0309 10.5569 20.0309C11.9435 20.0309 13.2026 20.0012 14.4421 19.9442C15.9153 19.8765 17.1258 18.7316 17.3226 17.27C17.451 16.3163 17.5569 15.3388 17.5569 14.3434C17.5569 13.348 17.451 12.3706 17.3226 11.4168C17.1258 9.9553 15.9153 8.81036 14.4421 8.74263C13.2026 8.68565 11.9435 8.65593 10.5569 8.65593C9.17027 8.65593 7.91112 8.68565 6.67159 8.74263C5.1985 8.81036 3.98799 9.9553 3.79122 11.4168C3.6628 12.3706 3.55688 13.348 3.55688 14.3434C3.55688 15.3388 3.6628 16.3163 3.79122 17.27Z"
                    stroke="#212529bf"
                    strokeWidth="1.2"
                  />
                  <path
                    d="M6.61938 8.65593V6.46843C6.61938 4.29381 8.38226 2.53093 10.5569 2.53093C12.7315 2.53093 14.4944 4.29381 14.4944 6.46843V8.65593"
                    stroke="#212529bf"
                    strokeWidth="1.2"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                  />
                  <path
                    d="M14.0569 14.3347V14.3447"
                    stroke="#212529bf"
                    strokeWidth="2"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                  />
                  <path
                    d="M10.5569 14.3347V14.3447"
                    stroke="#212529bf"
                    strokeWidth="2"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                  />
                  <path
                    d="M7.05688 14.3347V14.3447"
                    stroke="#212529bf"
                    strokeWidth="2"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                  />
                </svg>
                <input
                  type="password"
                  value={formData.password}
                  onChange={(x) =>
                    handleOnChange("password", x.currentTarget.value)
                  }
                  placeholder="ادخل كلمة المرور"
                />
              </label>
            </div>
            <div className="d-flex align-items-center gap-2 mb-5">
              <input
                type="checkbox"
                className="form-check-input"
                checked={formData.rememberMe}
                onChange={(x) =>
                  handleOnChange("rememberMe", x.currentTarget.checked)
                }
              />
              <label className="fw-semibold text-muted">تذكرني ؟</label>
            </div>
            <button
              className="custom-btn primary rounded-3 w-75 mx-auto"
              type="submit"
              disabled={
                loading ||
                formData.userName.trim() == "" ||
                formData.password.trim() == ""
              }
            >
              {loading ? "الرجاء الانتظار .." : "تسجيل الدخول"}
            </button>
          </form>
        </div> */}
      </div>
    </main>
  );
};

export default Login;
