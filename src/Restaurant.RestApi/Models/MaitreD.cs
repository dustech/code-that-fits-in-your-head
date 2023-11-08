namespace Dustech.Restaurant.RestApi.Models;

internal static class MaitreD
{
    internal static bool WillAccept(
            IEnumerable<Reservation> existingReservations,
            Reservation candidate)
    {
        int reservedSeats = existingReservations.Sum(r => r.Quantity);
        return reservedSeats + candidate.Quantity <= 10;
    }
}
