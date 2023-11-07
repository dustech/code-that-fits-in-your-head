using Dustech.Restaurant.RestApi.Models; // Reservation

namespace Dustech.Restaurant.RestApi.Tests;

public class ReservationTests
{
    [Theory(DisplayName = "QuantityMustBePositive")]
    [InlineData(0)]
    [InlineData(-1)]
    public void QuantityMustBePositive(int invalidQantity)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Reservation(
                new DateTime(2024, 8, 19, 11, 30, 0),
                "mail@example.com",
                "Tongolo Verace",
                invalidQantity));
    }
}
