using SprintBusiness.Whatsapp.Constant;
using SprintBusiness.Whatsapp.Constant.Interactive;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SprintBusiness.Whatsapp.Dtos
{
    public class SendMenuMessageDto
    {
        [JsonPropertyName("messaging_product")]
        public string MessagingProduct { get; set; } = "whatsapp";

        [JsonPropertyName("recipient_type")]
        public string RecipientType { get; } = RecipientTypes.Individual;

        [JsonPropertyName("to")]
        public required string To { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; } = MessageTypes.Interactive;

        [JsonPropertyName("interactive")]
        public required SendMenuMessageInteractiveDto Interactive { get; set; }
    }
    public class SendMenuMessageRowDto
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("title") , MaxLength(24)]
        public required string Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    public class SendMenuMessageActionDto
    {
        [JsonPropertyName("button") , MaxLength(20)]
        public required string Button { get; set; }

        [JsonPropertyName("sections")]
        public required List<SendMenuMessageSectionDto> Sections { get; set; }
    }

    public class SendMenuMessageBodyDto
    {
        [JsonPropertyName("text")]
        public required string Text { get; set; }
    }

    public class SendMenuMessageFooterDto
    {
        [JsonPropertyName("text")]
        public required string Text { get; set; }
    }

    public class SendMenuMessageHeaderDto
    {
        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("text")]
        public required string Text { get; set; }
    }

    public class SendMenuMessageInteractiveDto
    {
        [JsonPropertyName("type")]
        public string Type { get; } = InteractiveTypes.List; 

        [JsonPropertyName("header")]
        public SendMenuMessageHeaderDto? Header { get; set; }

        [JsonPropertyName("body")]
        public required SendMenuMessageBodyDto Body { get; set; }

        [JsonPropertyName("footer")]
        public SendMenuMessageFooterDto? Footer { get; set; }

        [JsonPropertyName("action")]
        public required SendMenuMessageActionDto Action { get; set; }
    }



    public class SendMenuMessageSectionDto
    {
        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [JsonPropertyName("rows")]
        public required List<SendMenuMessageRowDto> Rows { get; set; }
    }




}
