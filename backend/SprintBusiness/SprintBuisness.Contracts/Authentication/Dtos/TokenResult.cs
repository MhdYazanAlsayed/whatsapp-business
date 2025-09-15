namespace SprintBuisness.Contracts.Authentication.Dtos
{
    public class TokenResult
    {
        public required string RefreshToken { get; set; }
        public required DateTime ExpireDate { get; set; }
    }
}
