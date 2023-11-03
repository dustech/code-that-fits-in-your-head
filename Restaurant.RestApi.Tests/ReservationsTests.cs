using System.Diagnostics.CodeAnalysis; // to use SuppressMessage
using System.Text.Json; // to use JsonSerializer
using Dustech.Restaurant.RestApi.Controllers; // to use ReservationsController
using Dustech.Restaurant.RestApi.Dtos; // to use ReservationDto
using Dustech.Restaurant.RestApi.Models; // to use Reservation
using Microsoft.AspNetCore.Mvc.Testing; // to use WebApplicationFactory
using SysMed = System.Net.Http.Headers; // to use MediaTypeValue

namespace Dustech.Restaurant.RestApi.Tests;

public class ReservationsTests
{
  [Fact(DisplayName = "PostValidReservation")]
  public async Task PostValidReservation()
  {
    var response = await PostReservation(new
    {
      at = "2023-11-02 19:00",
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


  [Fact(DisplayName = "PostValidReservationWhenDatabaseIsEmpty")]
  public async Task PostValidReservationWhenDatabaseIsEmpty()
  {
    var db = new FakeDatabase();
    var sut = new ReservationsController(db);

    ReservationDto dto = new()
    {
      At = "2023-11-24 19:00",
      Email = "polonzomolofoloppo@polonzo.com",
      Name = "Polonzo, Mr.",
      Quantity = 4
    };


    await sut.Post(dto);

    var expected = new Reservation(
        new DateTime(2023, 11, 24, 19, 0, 0),
        dto.Email!,
        dto.Name!,
        dto.Quantity);
    Assert.Contains(expected, db);
  }

}
