namespace SprintBusiness.Shared.Helpers
{
    public class ApiEnvironment
    {
        public static bool IsDevelopment (string env)
        {
            return env == "Development";
        }

        public static string Url { get; set; } = null!;
    }
}
