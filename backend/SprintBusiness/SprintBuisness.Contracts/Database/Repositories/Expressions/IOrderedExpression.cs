namespace SprintBuisness.Contracts.Database.Repositories.Expressions
{
    public interface IOrderedExpression<T> : IBaseQueryableRepository<T> where T : class
    {
        IOrderedExpression<T> OrderBy(string property);
        IOrderedExpression<T> OrderByDesc(string property);
    }
}
