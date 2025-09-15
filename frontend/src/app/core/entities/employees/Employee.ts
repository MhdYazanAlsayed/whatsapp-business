export default interface Employee {
  id: EmployeeId;
  englishName: string;
  arabicName: string;
  email: string;
  phoneNumber: string;
  workGroupCount: number;
}

export interface EmployeeId {
  value: number;
}
