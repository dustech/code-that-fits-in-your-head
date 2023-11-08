using System.Globalization; // CultureInfo
using Dustech.Restaurant.RestApi.Dtos; // to use ReservationDto
using Dustech.Restaurant.RestApi.Interfaces; // to use IReservationsRepository
using Dustech.Restaurant.RestApi.Models; // to use Reservation
using Microsoft.AspNetCore.Mvc; // to use vApiController, Route

namespace Dustech.Restaurant.RestApi.Controllers;

[ApiController, Route("[controller]")]
public class ReservationsController(IReservationsRepository repository)
{
    private readonly MaitreD maitreD = new(new(TableType.Communal, 10));

    public IReservationsRepository Repository { get; } = repository ?? throw new ArgumentNullException(nameof(repository));

    //   [HttpPost]
    public async Task<ActionResult> Post(ReservationDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        var reservation = dto.Validate();

        if (null == reservation)
            return new BadRequestResult();


        var reservations = await Repository
                                .ReadReservations(reservation.At)
                                .ConfigureAwait(false);


        if (!maitreD.WillAccept(reservations, reservation))
            return new StatusCodeResult(
                StatusCodes.Status500InternalServerError);

        await Repository.Create(reservation).ConfigureAwait(false);

        return new NoContentResult();
    }


}
