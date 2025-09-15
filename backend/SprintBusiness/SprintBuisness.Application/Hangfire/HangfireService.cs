using System.Linq.Expressions;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using SprintBuisness.EntityframeworkCore.Contexts;
using SprintBusiness.Contracts.Hangfire;
using SprintBusiness.Domain.Hangfire;   

namespace SprintBusiness.Application.Hangfire;

public class HangfireService : IHangfireService
{
    private readonly ApplicationDbContext _context;
    public HangfireService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ScheduleAsync<T>(string tag, TimeSpan timeSpan , Expression<Action<T>> expression) 
    {
        var jobId = BackgroundJob.Schedule<T>(
                expression,
                timeSpan
            );

        await _context.HangfireTasks.AddAsync(new HangfireTask(tag, jobId));
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync (string tag)
    {
        var tasks = await _context.HangfireTasks.Where(x => x.Key == tag).ToListAsync();

        foreach (var task in tasks)
        {
            var result = BackgroundJob.Delete(task.TaskId);
            if (result)
                _context.HangfireTasks.Remove(task);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<HangfireTask?> GetAsync(string tag)
    {
        return await _context.HangfireTasks.FirstOrDefaultAsync(x => x.Key == tag);
    }
}