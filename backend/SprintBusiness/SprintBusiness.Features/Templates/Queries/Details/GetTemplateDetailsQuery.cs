using MediatR;
using SprintBusiness.Domain.Templates;
using SprintBusiness.Domain.Templates.Keys;

namespace SprintBusiness.Features.Templates.Queries.Details
{
    public class GetTemplateDetailsQuery : IRequest<Template?>
    {
        public GetTemplateDetailsQuery(int id)
        {
            Id = new(id);
        }

        public TemplateId Id { get; set; }
    }
}
