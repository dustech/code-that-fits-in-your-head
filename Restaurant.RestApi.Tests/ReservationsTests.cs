using System.Globalization; // CultureInfo
using System.Net; // HttpStatusCode
using Dustech.Restaurant.RestApi.Controllers; // to use ReservationsController
using Dustech.Restaurant.RestApi.Dtos; // to use ReservationDto
using Dustech.Restaurant.RestApi.Models; // to use Reservation


namespace Dustech.Restaurant.RestApi.Tests;

public class ReservationsTests
{
    [Fact(DisplayName = "PostValidReservation")]
    public async Task PostValidReservation()
    {
        using var service = new RestaurantApiFactory();
        var response = await service.PostReservation(new
        {
            at = "2023-11-02 19:00",
            email = "foo@bar.com",
            name = "Foo Bar Buzzzississi",
            quantity = 2
        });

        Assert.True(response.IsSuccessStatusCode, $"Actual status code: {response.StatusCode}.");
    }


    [Theory(DisplayName = "PostValidReservationWhenDatabaseIsEmpty")]
    [InlineData(
              "2023-11-06 19:00", "bongo@patrikrio.net", "Munzio Burzo", 5)]
    [InlineData("2023-11-13 18:15", "panico@example.com", "Panic Krg", 9)]
    [InlineData("2023-11-15 18:15", "validmail@v.it", null, 1)]
    [InlineData(
              "2022-03-18 17:30", "thesameemail@example.org", "Shanghai Li", 5)]
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

        Reservation expected = new(
            DateTime.Parse(dto.At, CultureInfo.InvariantCulture),
                dto.Email,
                dto.Name ?? "",
                dto.Quantity);
        Assert.Contains(expected, db);
    }

    [Theory(DisplayName = "PostInvalidReservation")]
    [InlineData(null, "j@example.net", "Jay Xerxes", 1)]
    [InlineData("not a date", "panino@calongoep.it", "Carmelo", 1)]
    [InlineData("2023-11-13 18:15", null, "Jay Xerxes", 1)]
    // senza la validazione del numero di posti il test fallisce perche il dto viene accettato
    [InlineData("2023-11-14 18:15", "validmail@v.it", "Per1", 0)]
    // senza la validazione del numero di posti il test fallisce perche il dto viene accettato
    [InlineData("2023-11-15 18:15", "validmail@v.it", "Per2", -1)]
    public async Task PostInvalidReservation(
    string at,
    string email,
    string name,
    int quantity)
    {
        using RestaurantApiFactory service = new();
        var response =
            await service.PostReservation(new { at, email, name, quantity });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    }

    [Fact(DisplayName = "OverbookAttempt")]
    public async Task OverbookAttempt()
    {
        using var service = new RestaurantApiFactory();
        await service.PostReservation(new
        {
            at = "2022-03-18 17:30",
            email = "bongo@t.com",
            name = "Marina Seminova",
            quantity = 6
        });

        var response = await service.PostReservation(new
        {
            at = "2022-03-18 17:30",
            email = "thesameemail@example.org",
            name = "Shanghai Li",
            quantity = 5
        });

        Assert.Equal(
            HttpStatusCode.InternalServerError,
            response.StatusCode);
    }
}
