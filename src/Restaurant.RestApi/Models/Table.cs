
namespace Dustech.Restaurant.RestApi.Models;

public record Table
{
    private Table(TableType tableType, int seats)
    {
        TableType = tableType;
        Seats = seats;
    }
    public TableType TableType { get; }
    public int Seats { get; }

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
        return new Table(TableType, newSeats);
    }
}
