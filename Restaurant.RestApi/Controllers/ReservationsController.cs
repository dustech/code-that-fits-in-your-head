using Dustech.Restaurant.RestApi.Dtos; // to use ReservationDto
using Dustech.Restaurant.RestApi.Interfaces; // to use IReservationsRepository
using Dustech.Restaurant.RestApi.Models; // to use Reservation
using Microsoft.AspNetCore.Mvc; // to use vApiController, Route

namespace Dustech.Restaurant.RestApi.Controllers;

[ApiController, Route("[controller]")]
public class ReservationsController(IReservationsRepository Repository) // : ControllerBase
{
    public IReservationsRepository Repository { get; } = Repository ?? throw new ArgumentNullException(nameof(Repository));

    //   [HttpPost]
    public async Task Post(ReservationDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        await Repository
            .Create(
                new Reservation(
                    At: new DateTime(2023, 11, 24, 19, 0, 0),
                    Email: "polonzomolofoloppo@polonzo.com",
                    Name: "Polonzo, Mr.",
                    Quantity: 4
                )
            )
            .ConfigureAwait(false);
    }
    // [HttpGet]
    // public IActionResult Get()
    // {
    //     var json = new
    //     {
    //         at = "2023-11-02 19:00",
    //         email = "foo@bar.com",
    //         name = "Foo Bar Buzzzississi",
    //         quantity = 2
    //     };

    //     return Ok(json);
    // }

}
