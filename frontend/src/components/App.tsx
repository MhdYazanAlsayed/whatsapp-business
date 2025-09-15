import { Fragment } from "react/jsx-runtime";
import Routes from "./Routes";
import { ToastContainer } from "react-toastify";
import DependenciesInjector from "src/app/core/util/DependenciesInjector";
import Loading from "./shared/react-select/Loading";
import "react-toastify/ReactToastify.min.css";

const _authenticator = DependenciesInjector.services.authenticator;

const App = () => {
  _authenticator.loadIdentity();

  return (
    <Fragment>
      <Routes />

      <Loading />
      <ToastContainer
        position="bottom-left"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={true}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme={"colored"}
      />
    </Fragment>
  );
};
export default App;
