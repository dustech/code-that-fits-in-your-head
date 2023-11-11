
namespace Dustech.Restaurant.RestApi.Models;

public record Table
{
    private Table(TableType tableType, int seats)
    {
        Seats = seats;
        IsStandard = tableType == TableType.Standard;
        IsCommunal = tableType == TableType.Communal;

    }
    public int Seats { get; }
    public bool IsStandard { get; }
    public bool IsCommunal { get; }

    public static Table Standard(int seats)
    {
        return new Table(TableType.Standard, seats);
    }

    public static Table Communal(int seats)
    {
        return new Table(TableType.Communal, seats);
    }

    public Table WithSeats(int newSeats)
    {
        return new Table(
            IsStandard ? TableType.Standard : TableType.Communal
            , newSeats);
    }
}
