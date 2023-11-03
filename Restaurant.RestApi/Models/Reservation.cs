namespace Dustech.Restaurant.RestApi.Models;

public sealed record Reservation(DateTime At, string Email, string Name, int Quantity);

