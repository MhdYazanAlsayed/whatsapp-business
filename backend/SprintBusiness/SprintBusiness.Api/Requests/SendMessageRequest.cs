using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Api.Requests
{
    public class SendMessageRequest
    {
        [Required]
        public string Content { get; set; } = null!;
    }
}
