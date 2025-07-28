
using Backend.API.Data;
using Backend.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
           
            builder.Services.AddDbContextFactory<AppDbContext>(options => 
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLstring")));

            builder.Services.AddScoped<ServiceTypesService>();
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    policy => policy.WithOrigins("http://localhost:53224").AllowAnyHeader().AllowAnyMethod());
            });
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowReactApp");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
