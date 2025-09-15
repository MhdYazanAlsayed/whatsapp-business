using System.Text.Json.Serialization;

namespace SprintBusiness.Whatsapp.Dtos
{
    public class UploadMediaResponseDto
    {
        [JsonPropertyName("id")]
        public string MediaId { get; set; } = null!;
    }
} 