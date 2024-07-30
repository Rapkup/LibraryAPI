using LibraryApi.Infrastructure;
using LibraryApi.Application;
using LibraryApi.Server.Middleware.CustomExceptionHandle;
using LibraryApi.Infrastructure.Authorization.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.BearerToken;
using LibraryApi.Infrastructure.Authorization.Models;
using Microsoft.OpenApi.Models;


namespace LibraryAPI.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter proper JWT token",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.Http
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "BearerAuth"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AuthContext>(opt => opt.UseSqlServer(connectionString));

            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<AuthContext>();

            builder.Services.ConfigureAll<BearerTokenOptions>(opt =>
            {
                opt.BearerTokenExpiration = TimeSpan.FromMinutes(1);
            });

            builder.Services
                .AddAplictaion()
                .AddInfrastructure(connectionString);


            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Global error handler
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.MapIdentityApi<IdentityUser>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();



            app.MapControllers();

            //app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
