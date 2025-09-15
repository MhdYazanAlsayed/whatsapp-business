import TemplateMessage from "src/app/core/entities/messages/templates/TemplateMessage";
import Employee from "src/app/core/entities/employees/Employee";

export default interface Props {
  data: TemplateMessage;
  sender: Employee;
}
