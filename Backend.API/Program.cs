
using Backend.API.Data;
using Backend.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.API
{
    public class Program
    {
        ///TODO: Comment code
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionStrings = builder.Environment.IsDevelopment() ? builder.Configuration.GetConnectionString("PostgreSQLstring") //for local dev
                : builder.Configuration.GetConnectionString("PostgreSQLstringDocker"); // for dock env

            builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseNpgsql(connectionStrings));

            builder.Services.AddHostedService<UpdateTimeslots>();
            builder.Services.AddScoped<ServiceTypesService>();
            builder.Services.AddScoped<BookingServices>();
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CustomerUI", policy =>
                {
                    policy.WithOrigins("https://localhost:7027")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var tries = 0;
                while (true)
                {
                    try
                    {
                        await context.Database.MigrateAsync();
                        Seeds.SeedDb.seedDb(context);                         
                        var updater = new UpdateTimeslots(scope.ServiceProvider);
                        await updater.MaintainTimeSlots(context);
                        break;
                    }
                    catch (Npgsql.NpgsqlException) when (tries++ < 10)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(5));
                    }
                }
            }

            var disableHttpsRedirect = (Environment.GetEnvironmentVariable("DISABLE_HTTPS_REDIRECT") ?? "")
            .Equals("true", StringComparison.OrdinalIgnoreCase);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (!disableHttpsRedirect)
            {
                app.UseHttpsRedirection();
            }

            app.UseCors("CustomerUI");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
