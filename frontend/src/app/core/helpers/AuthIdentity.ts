import { EmployeeId } from "../entities/employees/Employee";

export default interface AuthIdentity {
  id: EmployeeId;
  expirationDate: string;
  englishName: string;
  arabicName: string;
  email: string;
}
