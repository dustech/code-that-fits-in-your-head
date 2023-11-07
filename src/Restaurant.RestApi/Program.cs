using Dustech.Restaurant.RestApi.Interfaces; // to use IReservationsRepository
using Dustech.Restaurant.RestApi.Repositories; // SqlReservationsRepository
using Microsoft.Data.SqlClient; //SqlConnectionStringBuilder 


namespace Dustech.Restaurant.RestApi;

public sealed class Program
{
  private Program()
  {

  }
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    //commento per un futuro con Swagger
    // builder.Services.AddEndpointsApiExplorer();
    //commento per un futuro con Swagger
    // builder.Services.AddSwaggerGen();
    SqlConnectionStringBuilder sqlConnectionStringBuilder = new()
    {
      InitialCatalog = "Restaurant",
      MultipleActiveResultSets = true,
      Encrypt = true,
      TrustServerCertificate = true,
      ConnectTimeout = 10,

      DataSource = "localhost,1434",//Environment.GetEnvironmentVariable("DUSIP");
      UserID = "dustech",
      Password = Environment.GetEnvironmentVariable("SQLPASSWORD"), // password;
      PersistSecurityInfo = false
    };

    var connStr = sqlConnectionStringBuilder.ConnectionString;

    builder.Services.AddControllers(); // per usare UseEndpoints
    builder.Services.AddSingleton<IReservationsRepository>(
        new SqlReservationsRepository(connStr)
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

}
