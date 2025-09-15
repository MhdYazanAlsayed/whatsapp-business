using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Api.Requests.WorkGroups
{
    public class WorkGroupRequest
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
