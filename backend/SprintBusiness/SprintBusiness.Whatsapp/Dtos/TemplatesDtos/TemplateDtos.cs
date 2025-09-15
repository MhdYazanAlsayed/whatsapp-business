
using System.Text.Json.Serialization;

namespace SprintBusiness.Whatsapp.Dtos.TemplatesDtos
{
    public class WhatsAppButton
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } // Optional for URL-type buttons
    }

    public class WhatsAppComponent
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; } // For HEADER

        [JsonPropertyName("text")]
        public string Text { get; set; } // For BODY or FOOTER

        [JsonPropertyName("buttons")]
        public List<WhatsAppButton> Buttons { get; set; } // For BUTTONS

        [JsonPropertyName("example")]
        public WhatsAppExample Example { get; set; } // For examples in the template
    }

    public class WhatsAppExample
    {
        [JsonPropertyName("header_handle")]
        public List<string> HeaderHandle { get; set; } // Example for headers

        [JsonPropertyName("body_text")]
        public List<List<string>> BodyText { get; set; } // Example for body text
    }

    public class WhatsAppDatum
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("message_send_ttl_seconds")]
        public int MessageSendTtlSeconds { get; set; } // Optional if present in your JSON

        [JsonPropertyName("parameter_format")]
        public string ParameterFormat { get; set; }

        [JsonPropertyName("components")]
        public List<WhatsAppComponent> Components { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("sub_category")]
        public string SubCategory { get; set; } // Optional if present in your JSON

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class WhatsAppCursors
    {
        [JsonPropertyName("before")]
        public string Before { get; set; }

        [JsonPropertyName("after")]
        public string After { get; set; }
    }

    public class WhatsAppPaging
    {
        [JsonPropertyName("cursors")]
        public WhatsAppCursors Cursors { get; set; }
    }

    public class WhatsAppTemplateRoot
    {
        [JsonPropertyName("data")]
        public List<WhatsAppDatum> Data { get; set; }

        [JsonPropertyName("paging")]
        public WhatsAppPaging Paging { get; set; }
    }
}
