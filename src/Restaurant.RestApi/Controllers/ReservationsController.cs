using System.Globalization; // CultureInfo
using Dustech.Restaurant.RestApi.Dtos; // to use ReservationDto
using Dustech.Restaurant.RestApi.Interfaces; // to use IReservationsRepository
using Dustech.Restaurant.RestApi.Models; // to use Reservation
using Microsoft.AspNetCore.Mvc; // to use vApiController, Route

namespace Dustech.Restaurant.RestApi.Controllers;

[ApiController, Route("[controller]")]
public class ReservationsController(IReservationsRepository repository) // : ControllerBase
{
    public IReservationsRepository Repository { get; } = repository ?? throw new ArgumentNullException(nameof(repository));

    //   [HttpPost]
    public async Task<ActionResult> Post(ReservationDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        if (!IsValid(dto))
            return new BadRequestResult();

        var d = DateTime.Parse(dto.At!, CultureInfo.InvariantCulture);
        var reservations = await Repository.ReadReservations(d).ConfigureAwait(false);
        int reservedSeats = reservations.Sum(r => r.Quantity);
        if (10 < reservedSeats + dto.Quantity)
            return new StatusCodeResult(
                StatusCodes.Status500InternalServerError);

        Reservation reservation = new(
                    At: DateTime.Parse(dto.At!, CultureInfo.InvariantCulture),
                    Email: dto.Email!,
                    Name: dto.Name ?? "",
                    Quantity: dto.Quantity
                );

        await Repository.Create(reservation).ConfigureAwait(false);

        return new NoContentResult();
    }

    private static bool IsValid(ReservationDto dto)
    {
        return DateTime.TryParse(dto.At, CultureInfo.InvariantCulture, out _)
                && !(dto.Email is null)
                && 0 < dto.Quantity;
    }

}
