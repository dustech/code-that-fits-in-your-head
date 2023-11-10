using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Dustech.Restaurant.RestApi.Models; // MaitreD

namespace Dustech.Restaurant.RestApi.Tests;

public class MaitreDTests
{


    [Theory(DisplayName = "Accept")]
    [SuppressMessage("Performance",
                    "CA1861: Avoid constant arrays as arguments",
                    Justification = @"Nei test non e' rilevante
                    la perdita di prestazioni")]
    [InlineData(new int[] { 12 })]
    [InlineData(new int[] { 8, 11 })]
    public void Accept(int[] tableSeats)
    {
        var tables = tableSeats.Select(s => new Table(TableType.Communal, s));

        MaitreD sut = new(tables);

        Reservation r = new(
            At: DateTime.Parse("2023-11-08 18:00", CultureInfo.InvariantCulture),
            Email: "superpippo@ok.com",
            Name: "",
            Quantity: 11
        );

        var willAccept = sut.WillAccept(Array.Empty<Reservation>(), r);

        Assert.True(willAccept);
    }

    [Fact]
    public void Reject()
    {
        var sut = new MaitreD(
            new Table(TableType: TableType.Communal, 6),
            new Table(TableType: TableType.Communal, 6)
        );

        Reservation r = new(
            At: DateTime.Parse("2023-11-08 18:00", CultureInfo.InvariantCulture),
            Email: "superpippo@ok.com",
            Name: "",
            Quantity: 7
        );

        var actual = sut.WillAccept(Array.Empty<Reservation>(), r);

        Assert.False(actual);
    }


}
