using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SprintBusiness.Whatsapp.Dtos
{
    public class UploadMediaDto
    {
        [Required]
        public IFormFile File { get; set; } = null!;
    }
} 