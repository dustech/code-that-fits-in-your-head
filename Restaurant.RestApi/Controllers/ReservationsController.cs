using Dustech.Restaurant.RestApi.Dtos; // to use ReservationDto
using Dustech.Restaurant.RestApi.Interfaces; // to use IReservationsRepository
using Dustech.Restaurant.RestApi.Models; // to use Reservation
using Microsoft.AspNetCore.Mvc; // to use vApiController, Route

namespace Dustech.Restaurant.RestApi.Controllers;

[ApiController, Route("[controller]")]
public class ReservationsController(IReservationsRepository Repository)
{
    public IReservationsRepository Repository { get; } = Repository ?? throw new ArgumentNullException(nameof(Repository));

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

}
