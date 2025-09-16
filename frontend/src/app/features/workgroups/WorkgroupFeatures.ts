import FeatureList from "src/app/core/helpers/app_helpers/types/FeatureList";
import GetWorkGroupsHandler from "./queries/get-workgroups/GetWorkGroupsHandler";
import GetWorkGroupsQuery from "./queries/get-workgroups/GetWorkGroupsQuery";

export const WorkgroupFeatures: FeatureList[] = [
  {
    command: GetWorkGroupsQuery,
    handler: GetWorkGroupsHandler,
  },
];
