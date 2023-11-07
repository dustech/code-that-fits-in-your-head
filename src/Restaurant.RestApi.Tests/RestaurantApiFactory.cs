using System.Diagnostics.CodeAnalysis; // to use SuppressMessage
using Dustech.Restaurant.RestApi.Interfaces;
using Microsoft.AspNetCore.Hosting; // IWebHostBuilder
using Microsoft.AspNetCore.Mvc.Testing; // WebApplicationFactory
using Microsoft.Extensions.DependencyInjection; // AddSingleton
using Microsoft.Extensions.DependencyInjection.Extensions; // RemoveAll
using System.Text.Json; // to use JsonSerializer
using SysMed = System.Net.Http.Headers; // to use MediaTypeValue

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

    [SuppressMessage(
                "Usage",
                "CA2234:Pass system uri objects instead of strings",
                Justification = "URL isn't passed as variable, but as literal.")]
    public async Task<HttpResponseMessage> PostReservation(object reservation)
    {
        var client = CreateClient();

        string json = JsonSerializer.Serialize(reservation);
        SysMed.MediaTypeHeaderValue mediaTypeHeaderValue = new("application/json");
        using StringContent content = new(json, mediaTypeHeaderValue);


        return await client.PostAsync("reservations", content);
    }

}
