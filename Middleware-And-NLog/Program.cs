
using Middleware_And_NLog.Middlewares;
using NLog.Extensions.Logging;

namespace Middleware_And_NLog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddLogging(options=>{
                options.ClearProviders();
                options.SetMinimumLevel(LogLevel.Error);
            });
            builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();
            builder.Services.AddControllers();
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

            app.UseAuthorization();

            app.UseExceptionMiddleware();

            app.MapControllers();

            app.Run();
        }
    }
}
