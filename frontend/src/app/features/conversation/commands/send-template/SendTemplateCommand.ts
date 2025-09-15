import IRequest from "src/app/core/helpers/mediatR/IRequest";
import { SendTemplateCommandPayload } from "./payloads/SendTemplateCommandPayload";
import { ResultDto } from "src/app/core/helpers/ResultDto";
import Message from "src/app/core/entities/messages/Message";

export default class SendTemplateCommand extends IRequest<ResultDto<Message>> {
  constructor(readonly payload: SendTemplateCommandPayload) {
    super();
  }
}
