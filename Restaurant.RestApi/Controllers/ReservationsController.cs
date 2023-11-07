using System.Globalization; // CultureInfo
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
    public async Task<ActionResult> Post(ReservationDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        if (!DateTime.TryParse(dto.At, out var d))
            return new BadRequestResult();
        if (dto.Email is null)
            return new BadRequestResult();
        if (dto.Quantity < 1)
            return new BadRequestResult();

        var reservations = await Repository.ReadReservations(d).ConfigureAwait(false);
        int reservedSeats = reservations.Sum(r => r.Quantity);
        if (10 < reservedSeats + dto.Quantity)
            return new StatusCodeResult(
                StatusCodes.Status500InternalServerError);


        Reservation reservation = new(
                    At: DateTime.Parse(dto.At, CultureInfo.InvariantCulture),
                    Email: dto.Email,
                    Name: dto.Name ?? "",
                    Quantity: dto.Quantity
                );

        await Repository.Create(reservation).ConfigureAwait(false);

        return new NoContentResult();
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
