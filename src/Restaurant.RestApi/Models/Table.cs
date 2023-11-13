
namespace Dustech.Restaurant.RestApi.Models;

public record Table
{
    private Table(bool isStandard, int seats)
    {
        Seats = seats;
        IsStandard = isStandard;
        IsCommunal = !isStandard;

    }
    public int Seats { get; }
    public bool IsStandard { get; }
    public bool IsCommunal { get; }

    public static Table Standard(int seats)
    {
        return new Table(true, seats);
    }

    public static Table Communal(int seats)
    {
        return new Table(false, seats);
    }

    public Table WithSeats(int newSeats)
    {
        return new Table(IsStandard, newSeats);
    }

    internal Table Reserve(int seatsToReserve)
    {
        return WithSeats(Seats - seatsToReserve);
    }
}
