import { useState } from "react";
import DependenciesInjector from "src/app/core/util/DependenciesInjector";
import "../../styles/loading.css";

const _loadingService = DependenciesInjector.services.loadingService;

const Loading = () => {
  const [loading, setLoading] = useState(false);

  _loadingService.setLoading = setLoading;

  return (
    <div className="loading" data-status={loading ? "run" : "stop"}>
      <div className="loading-overlay"></div>
      <div className="loading-bar"></div>
    </div>
  );
};

export default Loading;
