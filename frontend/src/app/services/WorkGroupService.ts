import { toast } from "react-toastify";
import { IHttp } from "../core/contracts/IHttp";
import IWorkGroup from "../core/contracts/IWorkGroup";
import WorkGroup from "../core/entities/work-groups/WorkGroup";
import Employee from "../core/entities/employees/Employee";

export default class WorkGroupService implements IWorkGroup {
  _api = "api/work-groups";

  constructor(readonly _httpService: IHttp) {}

  async getAsync(): Promise<WorkGroup[]> {
    const response = await this._httpService.getData(this._api);
    if (!response || !response.ok) {
      toast.error("");
      return [];
    }

    return (await response.json()) as WorkGroup[];
  }

  async getMembersAsync(id: string): Promise<Employee[]> {
    const response = await this._httpService.getData(
      this._api + `/${id}/members`
    );

    if (!response.ok) {
      throw new Error();
    }

    return (await response.json()) as Employee[];
  }
}
