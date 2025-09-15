using SprintBusiness.Domain.Messages.Enums;

namespace SprintBusiness.Features.Conversations.Queries.GetConvertOptions
{
    public class ConvertOptionsResultDto
    {
        public string Id { get; set; } = null!;
        public string Text { get; set; } = null!;
        public ConversationOwner Type { get; set; }
    }
}
