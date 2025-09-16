import FeatureList from "src/app/core/helpers/app_helpers/types/FeatureList";
import GetAllTemplateQuery from "./queries/get-all/GetAllTemplatesQuery";
import GetAllTemplateQueryHandler from "./queries/get-all/GetAllTemplateQueryHandler";
import GetTemplateDetailsQuery from "./queries/details/GetTemplateDetailsQuery";
import GetTemplateDetailsQueryHandler from "./queries/details/GetTemplateDetailsQueryHandler";
import UploadTemplateMediaCommand from "./commands/upload-media/UploadTemplateMediaCommand";
import UploadTemplateMediaCommandHandler from "./commands/upload-media/UploadTemplateMediaCommandHandler";
import GetTemplatesPaginationQuery from "./queries/get-pagination/GetTemplatesPaginationQuery";
import GetTemplatesPaginationHandler from "./queries/get-pagination/GetTemplatesPaginationHandler";
import CreateTemplateCommand from "./commands/create/CreateTemplateCommand";
import CreateTemplateHandler from "./commands/create/CreateTemplateHandler";
export const TemplateFeatures: FeatureList[] = [
  {
    command: GetAllTemplateQuery,
    handler: GetAllTemplateQueryHandler,
  },
  {
    command: GetTemplateDetailsQuery,
    handler: GetTemplateDetailsQueryHandler,
  },
  {
    command: UploadTemplateMediaCommand,
    handler: UploadTemplateMediaCommandHandler,
  },
  {
    command: GetTemplatesPaginationQuery,
    handler: GetTemplatesPaginationHandler,
  },
  {
    command: CreateTemplateCommand,
    handler: CreateTemplateHandler,
  },
];
