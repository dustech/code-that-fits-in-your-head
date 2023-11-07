using Dustech.Restaurant.RestApi.Models;

namespace Dustech.Restaurant.RestApi.Interfaces;


public interface IReservationsRepository
{
    Task Create(Reservation reservation);
    Task<IReadOnlyCollection<Reservation>> ReadReservations(
            DateTime dateTime);
}