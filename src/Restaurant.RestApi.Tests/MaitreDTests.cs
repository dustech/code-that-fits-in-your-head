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
    [InlineData(new int[] { 12 }, new int[] { 0 }, 11)]
    [InlineData(new int[] { 8, 11 }, new int[] { 0 }, 11)]
    [InlineData(new int[] { 2, 11 }, new int[] { 2 }, 9)]
    public void Accept(int[] tableSeats, int[] reservedSeats, int candidateQuantity)
    {
        var tables = tableSeats.Select(s => new Table(TableType.Communal, s));
        var sut = new MaitreD(tables);

        var rs = reservedSeats.Where(s => s > 0).Select(s =>
            new Reservation(
                At: DateTime.Parse("2023-11-08 18:00", CultureInfo.InvariantCulture),
                Email: "superpippo@ok.com",
                Name: "",
                Quantity: s
            )
        );

        Reservation r = new(
            At: DateTime.Parse("2023-11-08 18:00", CultureInfo.InvariantCulture),
            Email: "superpippo@ok.com",
            Name: "",
            Quantity: candidateQuantity
        );

        var willAccept = sut.WillAccept(rs, r);

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
