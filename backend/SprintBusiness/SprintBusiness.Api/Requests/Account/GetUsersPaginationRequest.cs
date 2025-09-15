using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SprintBusiness.Api.Requests.Account
{
    public class GetEmployeesPaginationDto
    {
        [BindRequired]
        public int Page { get; set; }
        public string? Keyword { get; set; }
    }
}
