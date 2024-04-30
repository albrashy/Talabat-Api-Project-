using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using TalabatG08.Core.Entites.Identity;
using TalabatG08.Core.Repositories;
using TalabatG08.Repository;
using TalabatG08.Repository.Data;
using TalabatG08.Repository.Identity;
using TalabtG08.APIs.Errors;
using TalabtG08.APIs.Extentions;
using TalabtG08.APIs.Helpers;
using TalabtG08.APIs.Middelwares;

namespace TalabtG08.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Services  Works with DI 
            builder.Services.AddControllers();

            builder.Services.AddDbContext<StoreContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connection =builder.Configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(connection);   

            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddApplicationServices();

            builder.Services.AddIdentityService(builder.Configuration);




            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddSwaggerServices();

            builder.Services.AddCors(Options =>
            {
                Options.AddPolicy("MyPolicy", Options =>
                {
                    Options.AllowAnyHeader();
                    Options.AllowAnyMethod();
                    Options.WithOrigins(builder.Configuration["FrontBaseUrl"]);
                });
            });


            #endregion

            var app = builder.Build();


            //Explictly
            var Scope = app.Services.CreateScope(); //services scoped
            var Services = Scope.ServiceProvider;  //DI

            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = Services.GetRequiredService<StoreContext>(); //ask clr to create object from storeDbContext explicitly
                await dbContext.Database.MigrateAsync();

                await StoreContextSeed.SeedAsync(dbContext);
                var IdentityContext = Services.GetRequiredService<AppIdentityDbContext>();
                await IdentityContext.Database.MigrateAsync();


                var usermanger = Services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(usermanger);

            }
            catch (Exception ex)
            {

                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error occured during apply migration ");
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddelwares>();
                app.UseSwaggerMiddelWare();
            }

            

            app.UseStatusCodePagesWithRedirects("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}