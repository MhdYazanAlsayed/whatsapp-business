using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Features.ReplyTemplates.Commands.Create
{
    public class CreateReplyTemplateDto
    {
        [Required, MaxLength(255)]
        public string Title { get; set; } = null!;

        [Required, MaxLength(2000)]
        public string Content { get; set; } = null!;
    }
}
