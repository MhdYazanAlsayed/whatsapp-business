using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Features.Templates.Commands.UploadMedia
{
    public class UploadTemplateMediaDto
    {
        [Required]
        public IFormFile File { get; set; } = null!;
    }
}
