import Employee from "../entities/employees/Employee";
import WorkGroup from "../entities/work-groups/WorkGroup";

export default interface IWorkGroup {
  getAsync(): Promise<WorkGroup[]>;
  getMembersAsync(id: string): Promise<Employee[]>;
}
