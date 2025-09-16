import FeatureList from "src/app/core/helpers/app_helpers/types/FeatureList";
import ValidateCodeCommandHandler from "./commands/ValidateCode/ValidateCodeCommandHandler";
import ValidateCodeCommand from "./commands/ValidateCode/ValidateCodeCommand";
import GetAccountStatusCommand from "./queries/GetAccountStatus/GetAccountStatusCommand";
import GetAccountStatusCommandHandler from "./queries/GetAccountStatus/GetAccountStatusCommandHandler";

export const AccountFeatures: FeatureList[] = [
  { command: ValidateCodeCommand, handler: ValidateCodeCommandHandler },
  { command: GetAccountStatusCommand, handler: GetAccountStatusCommandHandler },
];
