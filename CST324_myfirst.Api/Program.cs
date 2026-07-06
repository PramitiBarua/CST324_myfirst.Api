
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

        private static void ConfigureApplication(WebApplication app)
        {
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("MyGuitarShop");
            //?? throw new InvalidOperationException("MyGuitarShop connectioon string not found. ");
            // Add services to the container.

            builder.Services.AddSingleton(new SqlConnectionFactory(connectionString));
            builder.Services.AddControllers();

        }
    }
}
