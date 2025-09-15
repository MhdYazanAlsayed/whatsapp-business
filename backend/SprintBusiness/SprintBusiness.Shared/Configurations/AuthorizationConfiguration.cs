namespace SprintBusiness.Shared.Configurations
{
    public class AuthorizationConfiguration
    {
        public bool ValidateIssuer { get; set; } = true;
        public bool ValidateAudience { get; set; } = true;
        public bool ValidateIssuerSigningKey { get; set; } = true;
        public bool RequireExpirationTime { get; set; } = false;
        public string Key { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Issuer { get; set; } = null!;
    }
}
