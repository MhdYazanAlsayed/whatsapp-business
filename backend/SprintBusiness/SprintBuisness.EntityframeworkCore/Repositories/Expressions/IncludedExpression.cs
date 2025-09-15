using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Database.Repositories.Expressions;
using SprintBusiness.Domain.Base;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Extensions;
using System.Linq.Expressions;

namespace SprintBuisness.EntityframeworkCore.Repositories.Expressions
{
    public class IncludedExpression<Type> : IIncludedExpression<Type> where Type : Entity
    {
        private List<string> _navigationProps = new List<string>();
        private readonly DbSet<Type> _table;

        public IncludedExpression(DbSet<Type> table)
        {
            _table = table;
        }

        public IIncludedExpression<Type> Include(string navigationProps)
        {
            _navigationProps.Add(navigationProps);

            return this;
        }

        public IConditionExpression<Type> Where(Expression<Func<Type, bool>> expression)
        {
            var result = new ConditionExpression<Type>(_navigationProps, _table);

            result.Where(expression);

            return result;
        }

        //public IOrderedExpression<Type> OrderBy(string prop)
        //{
        //    var ordered = new OrderExpression<Type>(_navigationProps, _table);
        //    ordered.OrderBy(prop);

        //    return ordered;
        //}

        //public IOrderedExpression<Type> OrderByDesc(string prop)
        //{
        //    var ordered = new OrderExpression<Type>(_navigationProps, _table);
        //    ordered.OrderByDesc(prop);

        //    return ordered;
        //}

        public async Task<IReadOnlyList<Type>> ListAsync()
        {
            var query = ExecuteInclude();

            return await query.ToListAsync();
        }

        public async Task<Type?> SingleOrDefaultAsync(Expression<Func<Type, bool>> expression)
        {
            var query = ExecuteInclude();

            return await query.SingleOrDefaultAsync(expression);
        }

        public async Task<Type> SingleAsync(Expression<Func<Type, bool>> expression)
        {
            var query = ExecuteInclude();

            return await query.SingleAsync(expression);
        }

        public async Task<Type?> FirstOrDefaultAsync(Expression<Func<Type, bool>> expression)
        {
            var query = ExecuteInclude();

            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<Type> FirstAsync(Expression<Func<Type, bool>> expression)
        {
            var query = ExecuteInclude();

            return await query.FirstAsync(expression);
        }

        public async Task<Type?> LastOrDefaultAsync(Expression<Func<Type, bool>> expression)
        {
            var query = ExecuteInclude();

            return await query.LastOrDefaultAsync(expression);
        }

        public async Task<Type> LastAsync(Expression<Func<Type, bool>> expression)
        {
            var query = ExecuteInclude();

            return await query.LastAsync(expression);
        }

        public async Task<bool> AnyAsync(Expression<Func<Type, bool>> expression)
        {
            var query = ExecuteInclude();

            return await query.AnyAsync(expression);
        }

        public async Task<PaginationDto<Type>> PaginationAsync(int page)
        {
            var query = ExecuteInclude();

            return await query.ToPaginationAsync(page);
        }

        private IQueryable<Type> ExecuteInclude()
        {
            IQueryable<Type> query = _table;

            foreach (var prop in _navigationProps)
            {
                query = query.Include(prop);
            }

            return query;
        }
    }

}
