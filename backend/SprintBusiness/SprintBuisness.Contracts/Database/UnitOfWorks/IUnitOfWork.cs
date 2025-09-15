using SprintBuisness.Contracts.Database.Repositories;
using SprintBuisness.Contracts.Markers;
using SprintBusiness.Domain.Contacts;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Flows;
using SprintBusiness.Domain.Flows.FlowMessageButtons;
using SprintBusiness.Domain.Flows.FlowMessageItems;
using SprintBusiness.Domain.Flows.FlowMessageLists;
using SprintBusiness.Domain.Flows.FlowMessages;
using SprintBusiness.Domain.Messages;

namespace SprintBuisness.Contracts.Database.UnitOfWorks
{
    public interface IUnitOfWork : IScopedDependency
    {
        public IAsyncRepository<Contact> Contacts { get; }
        public IAsyncRepository<Conversation> Conversations { get; }
        public IAsyncRepository<Flow> Flows { get; }
        public IAsyncRepository<FlowMessage> FlowMessages { get; }
        public IAsyncRepository<FlowMessageButton> FlowMessageButtons { get; }
        public IAsyncRepository<FlowMessageOption> FlowMessageOptions { get; }
        public IAsyncRepository<FlowMessageListItem> FlowMessageListItems { get; }
        public IAsyncRepository<Message> Messages { get; }

        Task SaveChangesAsync();
    }
}
