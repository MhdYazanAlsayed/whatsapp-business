import { toast } from "react-toastify";
import { IHttp } from "../core/contracts/IHttp";
import IUser from "../core/contracts/IUser";
import Employee from "../core/entities/employees/Employee";

export default class ApplicationUserService implements IUser {
  _api = "api/users";

  constructor(readonly _httpService: IHttp) {}

  async getAsync(): Promise<Employee[]> {
    const response = await this._httpService.getData(this._api);
    if (!response || !response.ok) {
      toast.error("");
      return [];
    }

    return (await response.json()) as Employee[];
  }
}
