﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TheKnife.API.Models.Settings;

namespace TheKnife.API.Configurations.Security
{
    public static class SecurityConfiguration
    {
        public static void AddCustomApiSecurity(this IServiceCollection services,
                                                ConfigurationManager configuration)
        {

            JwtSettings securitySettings = configuration.GetSection("Jwt").Get<JwtSettings>();
            services.AddSingleton(securitySettings);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = securitySettings.Issuer,
                    ValidAudience = securitySettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(securitySettings.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
        }
    }
}
