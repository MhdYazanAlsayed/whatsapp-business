import FeatureList from "src/app/core/helpers/app_helpers/types/FeatureList";
import GetReplyTemplatePaginationQuery from "./queries/get-pagination/GetReplyTemplatePaginationQuery";
import GetReplyTemplatePaginationQueryHandler from "./queries/get-pagination/GetReplyTemplatePaginationQueryHandler";
import GetReplyTemplateDetailsQuery from "./queries/details/GetReplyTemplateDetailsQuery";
import GetReplyTemplateDetailsQueryHandler from "./queries/details/GetReplyTemplateDetailsQueryHandler";
import CreateReplyTemplateCommand from "./commands/create/CreateReplyTemplateCommand";
import CreateReplyTemplateCommandHandler from "./commands/create/CreateReplyTemplateCommandHandler";
import UpdateReplyTemplateCommand from "./commands/update/UpdateReplyTemplateCommand";
import UpdateReplyTemplateCommandHandler from "./commands/update/UpdateReplyTemplateCommandHandler";

export const ReplyTemplateFeatures: FeatureList[] = [
  {
    command: GetReplyTemplatePaginationQuery,
    handler: GetReplyTemplatePaginationQueryHandler,
  },
  {
    command: GetReplyTemplateDetailsQuery,
    handler: GetReplyTemplateDetailsQueryHandler,
  },
  {
    command: CreateReplyTemplateCommand,
    handler: CreateReplyTemplateCommandHandler,
  },
  {
    command: UpdateReplyTemplateCommand,
    handler: UpdateReplyTemplateCommandHandler,
  },
];
