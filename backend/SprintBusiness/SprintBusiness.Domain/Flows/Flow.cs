using SprintBusiness.Domain.Base;
using SprintBusiness.Domain.Flows.Keys;

namespace SprintBusiness.Domain.Flows
{
    public class Flow : Entity
    {
        protected Flow()
        {
            Id = new FlowId(0);
            Title = string.Empty;
        }
        private Flow(string title)
        {
            Title = title;
        }

        public FlowId Id { get; private set; } = null!;
        public string Title { get; private set; } = null!;
        
        public static Flow Create (string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException();

            return new Flow(title);
        }
        public void Update (string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException();

            Title = title;
        }

    }
}
