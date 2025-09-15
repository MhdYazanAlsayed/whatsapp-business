using System.Linq.Expressions;
using SprintBuisness.Contracts.Markers;
using SprintBusiness.Domain.Hangfire;

namespace SprintBusiness.Contracts.Hangfire;
public interface IHangfireService : IScopedDependency
{
    Task ScheduleAsync<T>(string tag, TimeSpan timeSpan, Expression<Action<T>> expression);
    Task DeleteAsync(string tag);
    Task<HangfireTask?> GetAsync(string tag);
}
