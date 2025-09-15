import Employee from "../entities/employees/Employee";

export default interface IUser {
  getAsync(): Promise<Employee[]>;
}
