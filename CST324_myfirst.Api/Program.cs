
using Microsoft.AspNetCore.HttpLogging;
using MyGuitarShop.Data.Ado.Factories;
using System.Diagnostics;

namespace CST324_myfirst.Api
{
    public static class Program
    {
        public static async void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                //Add loing to the container
                AddLogging(builder);

                //Add services to the container
                AddServices(builder);

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                if (builder.Environment.IsDevelopment())
                {
                    builder.Services.AddEndpointsApiExplorer();
                    builder.Services.AddSwaggerGen();
                }


                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                ConfigureApplication(app);

                await app.RunAsync();
            }

            catch (Exception ex){
                if (Debugger.IsAttached) Debugger.Break();
                Console.WriteLine(ex.Message);
            }
            

               
        }

        private static void AddLogging(WebApplicationBuilder builder)
        {
            builder.Services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging
                .AddFilter("Microsoft", LogLevel.Information)
                .AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information)
                .AddConsole();
            });

            builder.Services.AddHttpLogging(options =>
            {
                options.LoggingFields = 
                                HttpLoggingFields.RequestPath | HttpLoggingFields.RequestMethod | HttpLoggingFields.ResponseStatusCode;
            });
        }

        private static void ConfigureApplication(WebApplication app)
        {
            app.UseHttpLogging();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("MyGuitarShop")
                ?? throw new InvalidOperationException("MyGuitarShop connectioon string not found. ");
            // Add services to the container.

            builder.Services.AddSingleton(new SqlConnectionFactory(connectionString));
            builder.Services.AddControllers();

        }
    }
}
