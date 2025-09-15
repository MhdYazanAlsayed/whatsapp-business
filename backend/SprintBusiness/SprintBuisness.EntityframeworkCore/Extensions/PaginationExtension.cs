using Microsoft.EntityFrameworkCore;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Shared.Extensions
{
    public static class PaginationExtension
    {
        public static async Task<PaginationDto<T>> ToPaginationAsync<T>(this IQueryable<T> query, int page , int? count = null) 
        {
            count = count ?? (await query.CountAsync());

            var pages = (int)Math.Ceiling(Convert.ToDecimal(count) / Convert.ToDecimal(20));

            var result = await query
                .Skip(20 * (page - 1))
                .Take(20)
                .ToListAsync();

            return new(result, pages);
        }
    }
}
