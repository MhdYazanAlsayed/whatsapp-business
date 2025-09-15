using MediatR;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Extensions;
using SprintBusiness.Domain.Contacts;
using SprintBusiness.Domain.Conversations;
using SprintBusiness.Domain.Conversations.Histories;
using SprintBusiness.Domain.Conversations.Notes;
using SprintBusiness.Domain.Conversations.Notes.Attachments;
using SprintBusiness.Domain.Flows;
using SprintBusiness.Domain.Flows.FlowMessageButtons;
using SprintBusiness.Domain.Flows.FlowMessageItems;
using SprintBusiness.Domain.Flows.FlowMessageLists;
using SprintBusiness.Domain.Flows.FlowMessages;
using SprintBusiness.Domain.Hangfire;
using SprintBusiness.Domain.Messages;
using SprintBusiness.Domain.Messages.Templates;
using SprintBusiness.Domain.ReplyTemplates;
using SprintBusiness.Domain.Templates;
using SprintBusiness.Domain.Templates.TemplateButtons;
using SprintBusiness.Domain.Templates.TemplateComponents;
using SprintBusiness.Domain.Templates.TemplateVariables;
using SprintBusiness.Domain.Users;
using SprintBusiness.Domain.Users.WorkGroups;

namespace SprintBuisness.EntityframeworkCore.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options , IMediator mediator): base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try 
            {
                await _mediator.DispatchDomainEventsAsync(this);

                return await base.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<FlowMessage> FlowMessages { get; set; }
        public DbSet<FlowMessageOption> FlowMessageOptions { get; set; }
        public DbSet<FlowMessageListItem> FlowMessageListItems { get; set; }
        public DbSet<FlowMessageButton> FlowMessageButtons { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ConversationHistory> ConversationHistories { get; set; }
        public DbSet<WorkGroup> WorkGroups { get; set; }
        public DbSet<ReplyTemplate> ReplyTemplates { get; set; }
        public DbSet<ConversationNote> ConversationNotes { get; set; }
        public DbSet<ConversationNoteAttachment> ConversationNoteAttachments { get; set; }
        public DbSet<Employee> Employees { get; set; }


        // Template
        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateButton> TemplateButtons { get; set; }
        public DbSet<TemplateVariable> TemplateVariables { get; set; }
        public DbSet<TemplateComponent> TemplateComponents { get; set; }
        public DbSet<TemplateMessage> TemplateMessages { get; set; }
        public DbSet<HangfireTask> HangfireTasks { get; set; }
    }
}
