using System.Globalization; // CultureInfo

namespace Dustech.Restaurant.RestApi.Dtos;

public class ReservationDto
{
    public string? At { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }

    internal bool IsValid =>
            DateTime.TryParse(At, CultureInfo.InvariantCulture, out _)
            && Email is not null
            && 0 < Quantity;

}
