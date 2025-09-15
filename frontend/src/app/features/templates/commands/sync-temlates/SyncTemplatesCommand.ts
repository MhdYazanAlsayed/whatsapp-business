import IRequest from "src/app/core/helpers/mediatR/IRequest";
import { SimpleResultDto } from "src/app/core/helpers/TaskResults";

export default class SyncTemplatesCommand extends IRequest<SimpleResultDto> {
  constructor() {
    // هذا الأمر لا يحتاج إلى أي بيانات إضافية حاليًا
    super();
  }
}
