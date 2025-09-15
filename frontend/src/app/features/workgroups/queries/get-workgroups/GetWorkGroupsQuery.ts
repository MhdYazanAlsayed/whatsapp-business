import IRequest from "src/app/core/helpers/mediatR/IRequest";
import WorkGroup from "src/app/core/entities/work-groups/WorkGroup";
export default class GetWorkGroupsQuery extends IRequest<WorkGroup[]> {
  constructor(public readonly keyword?: string) {
    super();
  }
}
