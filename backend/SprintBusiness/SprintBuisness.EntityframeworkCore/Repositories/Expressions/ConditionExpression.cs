using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Database.Repositories.Expressions;
using SprintBusiness.Domain.Base;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Extensions;
using System.Linq.Expressions;

namespace SprintBuisness.EntityframeworkCore.Repositories.Expressions
{
    public class ConditionExpression<Type> : IConditionExpression<Type> where Type : Entity
    {
        private List<string> _navigationProps;
        private readonly DbSet<Type> _table;
        private List<Expression<Func<Type, bool>>> expressions;
        public ConditionExpression(List<string> navigationProps, DbSet<Type> table)
        {
            _navigationProps = navigationProps;
            _table = table;
            expressions = new();
        }

        public IConditionExpression<Type> Where(Expression<Func<Type, bool>> expression)
        {
            expressions.Add(expression);

            return this;
        }

        public async Task<IReadOnlyList<Type>> ListAsync()
        {
            var query = ExecuteQuery();

            return await query.ToListAsync();
        }

        public async Task<Type?> SingleOrDefaultAsync(Expression<Func<Type, bool>> expression)
        {
            var query = ExecuteQuery();

            return await query.SingleOrDefaultAsync(expression);
        }

        public async Task<Type> SingleAsync(Expression<Func<Type, bool>> expression)
        {
            var query = ExecuteQuery(); 

            return await query.SingleAsync(expression);
        }

        public async Task<Type?> FirstOrDefaultAsync(Expression<Func<Type, bool>> expression)
        {
            var query = ExecuteQuery();

            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<Type> FirstAsync(Expression<Func<Type, bool>> expression)
        {
            var query = ExecuteQuery();

            return await query.FirstAsync(expression);
        }

        public async Task<Type?> LastOrDefaultAsync(Expression<Func<Type, bool>> expression)
        {
            var query = ExecuteQuery();

            return await query.LastOrDefaultAsync(expression);
        }

        public async Task<Type> LastAsync(Expression<Func<Type, bool>> expression)
        {
            var query = ExecuteQuery();

            return await query.LastAsync(expression);
        }

        public async Task<bool> AnyAsync(Expression<Func<Type, bool>> expression)
        {
            var query = ExecuteQuery();

            return await query.AnyAsync(expression);
        }

        public async Task<PaginationDto<Type>> PaginationAsync(int page)
        {
            IQueryable<Type>? query = _table;
            var count = await query.CountAsync();

            foreach (var condition in expressions)
            {
                query = query.Where(condition);
            }

            foreach (var prop in _navigationProps)
            {
                query = query.Include(prop);
            }

            return await query.ToPaginationAsync(page , count);
        }

        private IQueryable<Type> ExecuteQuery()
        {
            IQueryable<Type>? query = _table;

            foreach (var condition in expressions)
            {
                query = query.Where(condition);
            }

            foreach (var prop in _navigationProps)
            {
                query = query.Include(prop);
            }

            return query;
        }

    }

}
