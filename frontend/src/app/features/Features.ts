import FeatureList from "../core/helpers/mediatR/FeatureList";
import { AccountFeatures } from "./account/AccountFeatures";
import { ConversationNoteFeatures } from "./conversation-notes/ConversationNoteFeatures";
import { ConversationFeature } from "./conversation/ConversationFeatures";
import { EmployeeFeatures } from "./employee/EmployeeFeatures";
import { ReplyTemplateFeatures } from "./reply-templates/ReplyTemplateFeatures";
import { TemplateFeatures } from "./templates/TemplateFeatures";
import { WorkgroupFeatures } from "./workgroups/WorkgroupFeatures";

export const Features: FeatureList[] = [
  ...TemplateFeatures,
  ...ConversationFeature,
  ...ConversationNoteFeatures,
  ...ReplyTemplateFeatures,
  ...AccountFeatures,
  ...WorkgroupFeatures,
  ...EmployeeFeatures,
];
