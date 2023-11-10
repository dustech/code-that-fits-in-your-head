
namespace Dustech.Restaurant.RestApi.Models;

public sealed class MaitreD
{
    private readonly int capacity;
    public MaitreD(params Table[] tables)
    {
        ArgumentNullException.ThrowIfNull(tables);
        capacity = tables.Sum(t => t.Seats);
    }

    public MaitreD(IEnumerable<Table> tables) : this(tables.ToArray())
    {
        ArgumentNullException.ThrowIfNull(tables);
    }

    public bool WillAccept(
            IEnumerable<Reservation> existingReservations,
            Reservation candidate)
    {
        ArgumentNullException.ThrowIfNull(existingReservations);
        ArgumentNullException.ThrowIfNull(candidate);

        int reservedSeats = existingReservations.Sum(r => r.Quantity);
        return reservedSeats + candidate.Quantity <= capacity;
    }
}
