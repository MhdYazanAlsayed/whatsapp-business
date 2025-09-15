import { BrowserRouter, Routes as ReactRoutes, Route } from "react-router-dom";
import Login from "./auth/Login";
import DashboardLayout from "./layouts/DashboardLayout";
import Index from "./dashboard/Index";
import UserMessenger from "./dashboard/chat/pages/UserMessenger";
import BotMessenger from "./dashboard/chat/pages/BotMessenger";
import UnAssignedChats from "./dashboard/unassigned/UnAssignedChats";
import ReplyTemplates from "./settings/reply-templates/Index";
import LoginCallBack from "./auth/LoginCallBack";
import Templates from "./settings/templates/index/Index";
import SettingsLayout from "./layouts/settings/SettingsLayout";
import CreateTemplate from "./settings/templates/create/Create";

const Routes = () => {
  return (
    <BrowserRouter>
      <ReactRoutes>
        <Route element={<DashboardLayout />}>
          <Route path="/" element={<Index />} />
          <Route path="/conversations" element={<UserMessenger />} />
          <Route path="/conversations/:id" element={<UserMessenger />} />

          <Route path="/bot/conversations" element={<BotMessenger />} />
          <Route path="/bot/conversations/:id" element={<BotMessenger />} />

          <Route
            path="/unassigned/conversations/:groupId"
            element={<UnAssignedChats />}
          />
          <Route element={<SettingsLayout />}>
            <Route
              path="/settings/reply-templates"
              element={<ReplyTemplates />}
            />
          </Route>

          <Route path="/settings/templates" element={<Templates />} />
          <Route
            path="/settings/templates/create"
            element={<CreateTemplate />}
          />
        </Route>
        <Route path="/login" element={<Login />} />
        <Route path="/login/callback" element={<LoginCallBack />} />
      </ReactRoutes>
    </BrowserRouter>
  );
};

export default Routes;
