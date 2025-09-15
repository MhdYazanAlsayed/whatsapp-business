using SprintBuisness.Contracts.Markers;
using SprintBusiness.Contracts.Whatsapp.Dtos;
using SprintBusiness.Domain.Contacts;

namespace SprintBuisness.Contracts.Whatsapp
{
    public interface IWhatsappBot : IScopedDependency
    {
        Task GenarateAnAnswerAsync(GenarateAnswerDto dto);
        Task SendEvaluationMessageAsync(Contact contact);
    }
}
