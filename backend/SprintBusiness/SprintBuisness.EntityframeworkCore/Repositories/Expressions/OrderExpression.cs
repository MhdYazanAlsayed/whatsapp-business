//using Microsoft.EntityFrameworkCore;
//using SprintBuisness.Contracts.Database.Repositories.Expressions;
//using SprintBusiness.Shared.Dtos;
//using System.Linq;
//using System.Linq.Expressions;

//namespace SprintBuisness.EntityframeworkCore.Repositories.Expressions
//{
//    public class OrderExpression<Type> : IOrderedExpression<Type> where Type : class
//    {
//        private List<string> _includes = new();
//        private List<Func<object, object>> _order = new();
//        private List<Func<object, object>> _orderDesc = new();
//        private readonly DbSet<Type> _table;

//        public OrderExpression(DbSet<Type> table)
//        {
//            _table = table;
//        }

//        public OrderExpression(List<string> includes, DbSet<Type> table)
//        {
//            _includes = includes;
//            _table = table;
//        }

//        //public IOrderedExpression<Type> OrderBy<TSource , TKey>(Func<TSource, TKey> expression)
//        //{
//        //    _order.Add((expression as Func<object , object>)!);

//        //    return this;
//        //}

//        //public IOrderedExpression<Type> OrderByDesc(string property)
//        //{
//        //    _orderDesc.Add(property);

//        //    return this;
//        //}

//        public Task<IReadOnlyList<Type>> ListAsync()
//        {
//            var query = ExecuteInclude();

//            return await query.ToListAsync();
//        }

//        public Task<Type?> SingleOrDefaultAsync(Expression<Func<Type, bool>> expression)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Type> SingleAsync(Expression<Func<Type, bool>> expression)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Type?> FirstOrDefaultAsync(Expression<Func<Type, bool>> expression)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Type> FirstAsync(Expression<Func<Type, bool>> expression)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Type?> LastOrDefaultAsync(Expression<Func<Type, bool>> expression)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Type> LastAsync(Expression<Func<Type, bool>> expression)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> AnyAsync(Expression<Func<Type, bool>> expression)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<PaginationDto<Type>> PaginationAsync(int page)
//        {
//            throw new NotImplementedException();
//        }

//        private IQueryable<Type> ApplyChanges()
//        {
//            IQueryable<Type> query = _table;

//            foreach (var prop in _includes)
//            {
//                query = query.Include(prop);
//            }

//            IOrderedEnumerable<Type>? ordered = null;
//            foreach (var order in _order)
//            {
//                ordered = query.OrderBy(order);
//            }

//            return query;
//        }

//        //Func<TSource, TKey>
//    }
//}
