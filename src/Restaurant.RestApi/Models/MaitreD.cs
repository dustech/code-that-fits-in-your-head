
namespace Dustech.Restaurant.RestApi.Models;

public sealed class MaitreD
{
    public IEnumerable<Table> Tables { get; }
    public MaitreD(params Table[] tables)
    {
        ArgumentNullException.ThrowIfNull(tables);
        Tables = tables;
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

        List<Table> availableTables = Tables.ToList();
        foreach (var r in existingReservations)
        {
            var table = availableTables.Find(t => r.Quantity <= t.Seats);
            if (table is { })
            {
                availableTables.Remove(table);
                if (table.TableType == TableType.Communal)
                    availableTables.Add(
                        new Table(
                            table.TableType,
                            table.Seats - r.Quantity));
            }
        }
        return availableTables.Any(t => candidate.Quantity <= t.Seats);
    }
}
