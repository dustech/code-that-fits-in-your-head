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
}
