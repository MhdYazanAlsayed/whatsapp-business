using System.Linq.Expressions;

namespace SprintBuisness.Contracts.Database.Repositories.Expressions
{
    public interface IIncludedExpression<Type> : IBaseQueryableRepository<Type> where Type : class
    {
        IIncludedExpression<Type> Include(string navigationProps);
        IConditionExpression<Type> Where(Expression<Func<Type, bool>> expression);
    }
}
