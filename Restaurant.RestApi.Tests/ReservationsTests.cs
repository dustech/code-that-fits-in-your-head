using System.Diagnostics.CodeAnalysis; // to use SuppressMessage
using System.Text.Json; // to use JsonSerializer
using Microsoft.AspNetCore.Mvc.Testing; // to use WebApplicationFactory
using SysMed = System.Net.Http.Headers; // to use MediaTypeValue
namespace Dustech.Restaurant.RestApi.Tests;

public class ReservationsTests
{
  [Fact]
  public async Task PostValidReservation()
  {
    var response = await PostReservation(new
    {
      date = "2023-11-02 19:00",
      email = "foo@bar.com",
      name = "Foo Bar Buzzzississi",
      quantity = 2
    });

    Assert.True(response.IsSuccessStatusCode, $"Actual status code: {response.StatusCode}.");
  }

  [SuppressMessage(
              "Usage",
              "CA2234:Pass system uri objects instead of strings",
              Justification = "URL isn't passed as variable, but as literal.")]
  private static async Task<HttpResponseMessage> PostReservation(object reservation)
  {
    using WebApplicationFactory<Program> factory = new();
    var client = factory.CreateClient();

    string json = JsonSerializer.Serialize(reservation);
    SysMed.MediaTypeHeaderValue mediaTypeHeaderValue = new("application/json");
    using StringContent content = new(json, mediaTypeHeaderValue);


    return await client.PostAsync("reservations", content);
  }

}
