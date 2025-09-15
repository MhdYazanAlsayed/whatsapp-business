using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.ReplyTemplates.Keys;

namespace SprintBusiness.Domain.ReplyTemplates
{
    public class ReplyTemplate : Entity
    {
        protected ReplyTemplate()
        {
            //UserId = null!;
            Title = null!;
            Content = null!;
        }

        private ReplyTemplate(string title , string content)
        {
            //UserId = userId;
            Title = title;
            Content = content;
        }

        public ReplyTemplateId Id { get; private set; } = null!;

        //public ApplicationUser? User { get; private set; }
        //public string UserId { get; private set; }

        public string Title { get; private set; }
        public string Content { get; private set; }

        public static ReplyTemplate Create (string title , string content)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException();

            return new ReplyTemplate(title, content);
        }

        public void Update (string title , string content)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException();

            Title = title;
            Content = content;
        }
    }
}
