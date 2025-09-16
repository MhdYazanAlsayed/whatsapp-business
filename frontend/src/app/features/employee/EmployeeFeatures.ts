import FeatureList from "src/app/core/helpers/app_helpers/types/FeatureList";
import ChangeEmployeeStatusCommand from "./commands/change-status/ChangeEmployeeStatusCommand";
import ChangeEmployeeStatusCommandHandler from "./commands/change-status/ChangeEmployeeStatusCommandHandler";

export const EmployeeFeatures: FeatureList[] = [
  {
    command: ChangeEmployeeStatusCommand,
    handler: ChangeEmployeeStatusCommandHandler,
  },
];
