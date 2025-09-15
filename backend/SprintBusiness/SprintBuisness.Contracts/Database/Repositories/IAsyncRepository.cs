using SprintBuisness.Contracts.Database.Repositories.Expressions;
using SprintBusiness.Domain.Base;
using System.Linq.Expressions;

namespace SprintBuisness.Contracts.Database.Repositories
{
    public interface IAsyncRepository<Type>: IBaseQueryableRepository<Type> where Type : Entity
    {
        IIncludedExpression<Type> Include(string prop);
        IConditionExpression<Type> Where(Expression<Func<Type, bool>> expression);
        Task CreateAsync(Type entity);
        void Update(Type entity);
        void Delete(Type entity);
    }
}
