using SprintBusiness.Domain.Base;

namespace SprintBusiness.Domain.Hangfire;

public class HangfireTask 
{
    public HangfireTask(string key, string taskId)
    {
        Key = key;
        TaskId = taskId;
    }

    public int Id { get; set; }
    public string Key { get; set; }
    public string TaskId { get; set; }
}
