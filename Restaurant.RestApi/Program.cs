public class Program
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

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    { //commento per un futuro con Swagger
      // app.UseSwagger();
      // app.UseSwaggerUI();
      app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();


    app.MapGet("/", async context =>
      {
        await context.Response.WriteAsync(
      string.Format("Hello, World!")
      );
      }
    );

    app.Run();
  }
}
