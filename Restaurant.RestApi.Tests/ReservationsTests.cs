using System.Diagnostics.CodeAnalysis; // to use SuppressMessage
using System.Globalization; // CultureInfo
using System.Net; // HttpStatusCode
using System.Text.Json; // to use JsonSerializer
using Dustech.Restaurant.RestApi.Controllers; // to use ReservationsController
using Dustech.Restaurant.RestApi.Dtos; // to use ReservationDto
using Dustech.Restaurant.RestApi.Models; // to use Reservation
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
        using RestaurantApiFactory factory = new();
        var client = factory.CreateClient();

        string json = JsonSerializer.Serialize(reservation);
        SysMed.MediaTypeHeaderValue mediaTypeHeaderValue = new("application/json");
        using StringContent content = new(json, mediaTypeHeaderValue);


        return await client.PostAsync("reservations", content);
    }


    [Theory(DisplayName = "PostValidReservationWhenDatabaseIsEmpty")]
    [InlineData(
              "2023-11-06 19:00", "bongo@patrikrio.net", "Munzio Burzo", 5)]
    [InlineData("2023-11-13 18:15", "panico@example.com", "Panic Krg", 9)]
    public async Task PostValidReservationWhenDatabaseIsEmpty(string at,
              string email,
              string name,
              int quantity)
    {
        var db = new FakeDatabase();
        var sut = new ReservationsController(db);

        var dto = new ReservationDto
        {
            At = at,
            Email = email,
            Name = name,
            Quantity = quantity
        };


        await sut.Post(dto);

        var expected = new Reservation(
            DateTime.Parse(dto.At, CultureInfo.InvariantCulture),
                dto.Email,
                dto.Name,
                dto.Quantity);
        Assert.Contains(expected, db);
    }

    [Theory]

    [InlineData(null, "j@example.net", "Jay Xerxes", 1)]
    public async Task PostInvalidReservation(
    string at,
    string email,
    string name,
    int quantity)
    {
        var response =
            await PostReservation(new { at, email, name, quantity });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    }
}
