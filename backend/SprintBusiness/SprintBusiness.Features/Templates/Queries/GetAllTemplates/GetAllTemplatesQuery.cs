using MediatR;
using SprintBusiness.Domain.Templates;

namespace SprintBusiness.Features.Templates.Queries.GetAllTemplates
{
    public class GetAllTemplatesQuery : IRequest<List<Template>>
    {
    }
}
