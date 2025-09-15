
using Microsoft.AspNetCore.Mvc;

namespace SprintBusiness.Api.Requests
{
    public class WebhookTestRequest
    {
        [FromQuery(Name = "hub.mode")]
        public string? Mode { get; set; }

        [FromQuery(Name = "hub.challenge")]
        public int? Challange { get; set; }

        [FromQuery(Name = "hub.verify_token")]
        public string? Token { get; set; }
    }
}
