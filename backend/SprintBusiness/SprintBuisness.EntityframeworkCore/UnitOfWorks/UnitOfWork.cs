using SprintBuisness.Contracts.Database.Repositories;
using SprintBuisness.Contracts.Database.UnitOfWorks;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Domain.Contacts;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Flows;
using SprintBusiness.Domain.Flows.FlowMessageButtons;
using SprintBusiness.Domain.Flows.FlowMessageItems;
using SprintBusiness.Domain.Flows.FlowMessageLists;
using SprintBusiness.Domain.Flows.FlowMessages;
using SprintBusiness.Domain.Messages;

namespace SprintBuisness.EntityframeworkCore.UnitOfWorks
{
    public class UnitOfWork : BaseUnitOfWork , IUnitOfWork
    {

        // Decalre repositories
        private IAsyncRepository<Contact>? _contacts;
        private IAsyncRepository<Conversation>? _conversations;
        private IAsyncRepository<Flow>? _flows;
        private IAsyncRepository<FlowMessage>? _flowMessages;
        private IAsyncRepository<FlowMessageButton>? _flowMessageButtons;
        private IAsyncRepository<FlowMessageListItem>? _flowMessageListItems;
        private IAsyncRepository<FlowMessageOption>? _flowMessageOptions;
        private IAsyncRepository<Message>? _messages;

        public UnitOfWork(ApplicationDbContext context): base(context)
        {
        }

        // Getter of repositories
        public IAsyncRepository<Contact> Contacts => GetRepository(ref _contacts);
        public IAsyncRepository<Conversation> Conversations => GetRepository(ref _conversations);
        public IAsyncRepository<Flow> Flows => GetRepository(ref _flows);
        public IAsyncRepository<FlowMessage> FlowMessages => GetRepository(ref _flowMessages);
        public IAsyncRepository<FlowMessageButton> FlowMessageButtons => 
            GetRepository(ref _flowMessageButtons);
        public IAsyncRepository<FlowMessageOption> FlowMessageOptions => GetRepository(ref _flowMessageOptions);

        public IAsyncRepository<FlowMessageListItem> FlowMessageListItems => GetRepository(ref _flowMessageListItems);
        public IAsyncRepository<Message> Messages => GetRepository(ref _messages);

    }
}
