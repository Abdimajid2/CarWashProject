
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
 
            var connectionStrings = Environment.GetEnvironmentVariable("ConnectionStrings__PostgreSQLstringDocker")
                ?? Environment.GetEnvironmentVariable("DATABASE_URL") ?? (builder.Environment.IsDevelopment()
                    ? builder.Configuration.GetConnectionString("PostgreSQLstring")
                    : builder.Configuration.GetConnectionString("PostgreSQLstringDocker"));

            Console.WriteLine($"Raw connection string: {connectionStrings}");
            Console.WriteLine($"ENV ConnectionStrings__PostgreSQLstringDocker: {Environment.GetEnvironmentVariable("ConnectionStrings__PostgreSQLstringDocker")}");
            Console.WriteLine($"ENV DATABASE_URL: {Environment.GetEnvironmentVariable("DATABASE_URL")}");

            if (!string.IsNullOrEmpty(connectionStrings) && connectionStrings.StartsWith("postgres://"))
            {
                var uri = new Uri(connectionStrings.Replace("postgres://", "postgresql://"));
                var userInfo = uri.UserInfo.Split(':');
                connectionStrings = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.Trim('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
            }
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
