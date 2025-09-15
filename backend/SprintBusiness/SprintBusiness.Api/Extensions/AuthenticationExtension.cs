using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SprintBusiness.Api.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme =
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["AuthSettings:Audience"],
                    ValidIssuer = configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"]!)),
                    ValidateIssuerSigningKey = true
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var path = context.HttpContext.Request.Path;

                        if (context.HttpContext.Request.Cookies.TryGetValue("access_token" , out var accessToken))
                        {
                            context.Token = accessToken;
                        }

                        if (context.HttpContext.Request.Cookies.TryGetValue("refresh_token", out var refreshToken))
                        {
                            // Send request to IDB server to get new access token
                        }


                        //if (!string.IsNullOrEmpty(accessToken) && 
                        //    path.StartsWithSegments("/chat") || path.StartsWithSegments("/chat-conversation"))
                        //{
                        //}

                        return Task.CompletedTask;
                    }
                };

            });

            return services;
        }
    }
}
