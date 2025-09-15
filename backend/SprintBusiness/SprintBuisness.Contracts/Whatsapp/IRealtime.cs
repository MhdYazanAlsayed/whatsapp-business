using SprintBuisness.Contracts.Markers;
using SprintBuisness.Contracts.Whatsapp.Dtos;

namespace SprintBuisness.Contracts.Whatsapp
{
    public interface IRealtime : IScopedDependency
    {
        Task SendMessageAsync(RealtimeMessageDto dto , List<int>? userIds = null);
        Task UpdateConversationAsync(ConversationUpdateDto dto);
    }
}
