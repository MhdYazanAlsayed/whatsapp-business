using MediatR;
using SprintBusiness.Shared.Dtos;
using System.Text.Json.Serialization;

namespace SprintBusiness.Features.Whatsapp.Webhooks.ReceiveMessage
{
    public class ReceiveMessageCommand : IRequest<ResultDto>
    {
        [JsonPropertyName("object")]
        public string Object { get; set; } = null!;

        [JsonPropertyName("entry")]
        public List<Entry> Entry { get; set; } = null!;
    }

    public class Change
    {
        [JsonPropertyName("value")]
        public Value Value { get; set; } = null!;

        [JsonPropertyName("field")]
        public string Field { get; set; } = null!;
    }

    public class Entry
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("changes")]
        public List<Change> Changes { get; set; } = null!;
    }

    public class Metadata
    {
        [JsonPropertyName("display_phone_number")]
        public string DisplayPhoneNumber { get; set; } = null!;

        [JsonPropertyName("phone_number_id")]
        public string PhoneNumberId { get; set; } = null!;
    }


    public class Value
    {
        [JsonPropertyName("messaging_product")]
        public string MessagingProduct { get; set; } = null!;

        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; } = null!;

        [JsonPropertyName("contacts")]
        public List<ContactResponse> Contacts { get; set; } = null!;

        [JsonPropertyName("messages")]
        public List<MessageResponse> Messages { get; set; } = null!;
    }

    public class ContactResponse
    {
        [JsonPropertyName("profile")]
        public Profile Profile { get; set; } = null!;

        [JsonPropertyName("wa_id")]
        public string WaId { get; set; } = null!;
    }

    public class Profile
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
    }

    public class MessageResponse
    {
        [JsonPropertyName("context")]
        public object? Context { get; set; } 

        [JsonPropertyName("from")]
        public string From { get; set; } = null!;

        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; } = null!;

        [JsonPropertyName("type")]
        public string Type { get; set; } = null!;

        [JsonPropertyName("interactive")]
        public Interactive? Interactive { get; set; }

        [JsonPropertyName("text")]
        public Text? Text { get; set; }
    }

    public class Interactive
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; } // Button | List

        [JsonPropertyName("button_reply")]
        public ButtonReply? ButtonReply { get; set; }

        [JsonPropertyName("list_reply")]
        public ListReply? ListReply { get; set; } 
    }

    public class ButtonReply
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;
    }

    public class ListReply
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; } = null!;
    }

    public class Text
    {
        [JsonPropertyName("body")]
        public string Body { get; set; } = null!;
    }
}
