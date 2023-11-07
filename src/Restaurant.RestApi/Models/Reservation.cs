namespace Dustech.Restaurant.RestApi.Models;

public sealed record Reservation(DateTime At, string Email, string Name, int Quantity)
{

    public int Quantity { get; } = Quantity < 1 ?
        throw new ArgumentOutOfRangeException(nameof(Quantity), "The value must be a positive (non-zero) number.") :
        Quantity;
}

