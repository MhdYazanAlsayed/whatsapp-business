using Microsoft.EntityFrameworkCore;
using SprintBuisness.Contracts.Database.Repositories;
using SprintBuisness.Contracts.Database.Repositories.Expressions;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBuisness.EntityframeworkCore.Repositories.Expressions;
using SprintBusiness.Domain.Base;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Extensions;
using SprintBusiness.Shared.Helpers;
using SprintBusiness.Shared.Interfaces;
using System.Linq.Expressions;

namespace SprintBuisness.EntityframeworkCore.Repositories
{
    public class AsyncRepository<Type> : IAsyncRepository<Type> where Type : Entity
    {
        private readonly DbSet<Type> _table;

        public AsyncRepository(ApplicationDbContext context)
        {
            _table = context.Set<Type>();
        }

        public IIncludedExpression<Type> Include(string prop)
        {
            var result = new IncludedExpression<Type>(_table);

            result.Include(prop);

            return result;
        }

        public IConditionExpression<Type> Where(Expression<Func<Type, bool>> expression)
        {
            var result = new ConditionExpression<Type>(new List<string>(), _table);

            result.Where(expression);

            return result;
        }

        public async Task<Type?> SingleOrDefaultAsync(Expression<Func<Type, bool>> expression)
        {
            return await _table.SingleOrDefaultAsync(expression);
        }

        public async Task<Type> SingleAsync(Expression<Func<Type, bool>> expression)
        {
            return await _table.SingleAsync(expression);
        }

        public async Task<Type?> FirstOrDefaultAsync(Expression<Func<Type, bool>> expression)
        {
            return await _table.FirstOrDefaultAsync(expression);
        }

        public async Task<Type> FirstAsync(Expression<Func<Type, bool>> expression)
        {
            return await _table.FirstAsync(expression);
        }

        public async Task<Type?> LastOrDefaultAsync(Expression<Func<Type, bool>> expression)
        {
            return await _table.LastOrDefaultAsync(expression);
        }

        public async Task<Type> LastAsync(Expression<Func<Type, bool>> expression)
        {
            return await _table.LastAsync(expression);
        }

        public async Task<bool> AnyAsync(Expression<Func<Type, bool>> expression)
        {
            return await _table.AnyAsync(expression);
        }

        public async Task<PaginationDto<Type>> PaginationAsync(int page)
        {
            return await _table.ToPaginationAsync(page);
        }

        public async Task<IReadOnlyList<Type>> ListAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task CreateAsync(Type entity)
        {
            await _table.AddAsync(entity);
        }

        public void Delete(Type entity)
        {
            _table.Remove(entity);
        }

        public void Update(Type entity)
        {
            _table.Update(entity);
        }
    }
}
