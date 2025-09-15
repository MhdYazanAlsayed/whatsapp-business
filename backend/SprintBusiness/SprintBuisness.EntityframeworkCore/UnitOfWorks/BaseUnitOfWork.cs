using SprintBuisness.Contracts.Database.Repositories;
using SprintBuisness.Contracts.Database.UnitOfWorks;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBuisness.EntityframeworkCore.Repositories;
using SprintBusiness.Domain.Base;

namespace SprintBuisness.EntityframeworkCore.UnitOfWorks
{
    public class BaseUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public BaseUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected IAsyncRepository<T> GetRepository<T>(ref IAsyncRepository<T>? repository) where T : Entity
        {
            return repository ?? (repository = new AsyncRepository<T>(_context));
        }
    }
}
