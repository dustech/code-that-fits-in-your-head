using System.Globalization;
using Dustech.Restaurant.RestApi.Models; // MaitreD

namespace Dustech.Restaurant.RestApi.Tests;

public class MaitreDTests
{
    [Fact(DisplayName = "Accept")]
    public void Accept()
    {
        MaitreD maitreD = new(new(TableType.Communal, 10));

        Reservation sut = new(
            At: DateTime.Parse("2023-11-08 18:00", CultureInfo.InvariantCulture),
            Email: "superpippo@ok.com",
            Name: "",
            Quantity: 11
        );

        var willAccept = maitreD.WillAccept(Array.Empty<Reservation>(), sut);

        Assert.True(willAccept);
    }
}
