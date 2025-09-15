// GetTemplateDetailsQueryPayload.ts

import TemplateId from "src/app/core/entities/templates/keys/TemplateId";

export interface GetTemplateDetailsQueryPayload {
  templateId: TemplateId; // معرف القالب الذي نريد الحصول على تفاصيله
}
