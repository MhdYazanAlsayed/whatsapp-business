using SprintBusiness.Domain.Messages.Enums;
using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Api.Requests
{
    public class ConvertConversationRequest
    {
        [Required]
        public ConversationOwner To { get; set; }
        public int? EmployeeId { get; set; }
        public int? WorkGroupId { get; set; }
    }
}
