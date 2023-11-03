using Dustech.Restaurant.RestApi.Interfaces; // to use IReservationsRepository
using Dustech.Restaurant.RestApi.Models; // to use Reservation


namespace Dustech.Restaurant.RestApi;

public sealed class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //commento per un futuro con Swagger
        // builder.Services.AddEndpointsApiExplorer();
        //commento per un futuro con Swagger
        // builder.Services.AddSwaggerGen();
        builder.Services.AddControllers(); // per usare UseEndpoints
        builder.Services.AddSingleton<IReservationsRepository>(
            new NullRepository()
        );
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        { //commento per un futuro con Swagger
          // app.UseSwagger();
          // app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        //app.UseHttpsRedirection();
        app.UseRouting();

#pragma warning disable ASP0014
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
#pragma warning restore ASP0014
        app.Run();
    }

    private sealed class NullRepository : IReservationsRepository
    {
        public Task Create(Reservation reservation)
        {
            return Task.CompletedTask;
        }
    }
}
