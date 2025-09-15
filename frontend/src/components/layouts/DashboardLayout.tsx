import { Navigate, Outlet } from "react-router-dom";
import DashboardHeader from "./headers/dashboard/DashboardHeader";
import DashboardSidebar from "./sidebars/DashboardSidebar";
import DependenciesInjector from "src/app/core/util/DependenciesInjector";

const _authenticator = DependenciesInjector.services.authenticator;

const DashboardLayout = () => {
  if (!_authenticator.isAuthenticated) return <Navigate to="/login" />;

  return (
    <div>
      <DashboardHeader />

      <main className="dashboard">
        <div className="container-fluid">
          <DashboardSidebar />
          <Outlet />
        </div>
      </main>
    </div>
  );
};

export default DashboardLayout;
