namespace Dustech.Restaurant.RestApi.Models;

public sealed record Reservation(DateTime At, string Email, string Name, int Quantity)
{

    public int Quantity { get; } = Quantity < 1 ?
        throw new ArgumentOutOfRangeException(nameof(Quantity), "The value must be a positive (non-zero) number.") :
        Quantity;

    public Reservation WithDate(DateTime newAt)
    {
        return new Reservation(newAt, Email, Name, Quantity);
    }

    public Reservation WithEmail(string newEmail)
    {
        return new Reservation(At, newEmail, Name, Quantity);
    }

    public Reservation WithName(string newName)
    {
        return new Reservation(At, Email, newName, Quantity);
    }

    public Reservation WithQuantity(int newQuantity)
    {
        return new Reservation(At, Email, Name, newQuantity);
    }
}

