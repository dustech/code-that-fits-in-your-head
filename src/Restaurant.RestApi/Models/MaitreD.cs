
namespace Dustech.Restaurant.RestApi.Models;

public sealed class MaitreD(Table table)
{
    public bool WillAccept(
            IEnumerable<Reservation> existingReservations,
            Reservation candidate)
    {
        ArgumentNullException.ThrowIfNull(existingReservations);
        ArgumentNullException.ThrowIfNull(candidate);

        int reservedSeats = existingReservations.Sum(r => r.Quantity);
        return reservedSeats + candidate.Quantity <= table.Seats;
    }
}
