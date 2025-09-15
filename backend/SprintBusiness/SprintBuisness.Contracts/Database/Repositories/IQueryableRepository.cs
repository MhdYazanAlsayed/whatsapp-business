using SprintBuisness.Contracts.Database.Repositories.Expressions;

namespace SprintBuisness.Contracts.Database.Repositories
{
    public interface IQueryableRepository<Type> : IBaseQueryableRepository<Type> where Type : class
    {
        public IQueryable<Type>? Query { get; set; }
    }
}
