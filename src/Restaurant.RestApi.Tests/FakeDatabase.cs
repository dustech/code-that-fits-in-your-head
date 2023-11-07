using System.Collections.ObjectModel; // to use Collection
using Dustech.Restaurant.RestApi.Interfaces; // to use IReservationsRepository
using Dustech.Restaurant.RestApi.Models; // to use Reservation

namespace Dustech.Restaurant.RestApi.Tests;

public class FakeDatabase : Collection<Reservation>, IReservationsRepository
{
    public Task Create(Reservation reservation)
    {
        Add(reservation);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Reservation>> ReadReservations(
            DateTime dateTime)
    {
        var min = dateTime.Date;
        var max = min.AddDays(1).AddTicks(-1);

        return Task.FromResult<IReadOnlyCollection<Reservation>>(
            this.Where(r => min <= r.At && r.At <= max).ToList());
    }
}
