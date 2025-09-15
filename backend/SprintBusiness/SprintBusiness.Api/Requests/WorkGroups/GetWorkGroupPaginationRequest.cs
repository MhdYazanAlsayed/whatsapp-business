using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SprintBusiness.Api.Requests.WorkGroups
{
    public class GetWorkGroupPaginationRequest
    {
        [BindRequired]
        public int Page { get; set; }
        public string? Keyword { get; set; }
    }
}
