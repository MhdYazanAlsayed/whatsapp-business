using System.Linq.Expressions;

namespace SprintBuisness.Contracts.Database.Repositories.Expressions
{
    public interface IConditionExpression<Type> : IBaseQueryableRepository<Type> where Type : class
    {
        IConditionExpression<Type> Where(Expression<Func<Type, bool>> expression);
    }
}
