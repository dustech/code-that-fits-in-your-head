using Dustech.Restaurant.RestApi.Interfaces;
using Microsoft.AspNetCore.Hosting; // IWebHostBuilder
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions; // WebApplicationFactory

namespace Dustech.Restaurant.RestApi.Tests;

public class RestaurantApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IReservationsRepository>();
            services.AddSingleton<IReservationsRepository>(
                 new FakeDatabase());
        });
    }
}
