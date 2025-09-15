using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SprintBusiness.Api.Requests.Conversations
{
    public class GetWorkGroupCovnersationsRequest
    {
        [BindRequired]
        public int Page { get; set; }
    }
}
