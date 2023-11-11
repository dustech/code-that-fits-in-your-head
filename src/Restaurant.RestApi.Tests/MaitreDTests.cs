using System.Diagnostics.CodeAnalysis; // SuppressMessage
using Dustech.Restaurant.RestApi.Models; // MaitreD

namespace Dustech.Restaurant.RestApi.Tests;

public class MaitreDTests
{


    [Theory(DisplayName = "Accept")]
    [InlineData(new int[] { 12 }, new int[] { 0 })]
    [InlineData(new int[] { 8, 11 }, new int[] { 0 })]
    [InlineData(new int[] { 2, 13 }, new int[] { 2 })]
    public void Accept(int[] tableSeats, int[] reservedSeats)
    {
        var tables = tableSeats.Select(Table.Communal);
        var sut = new MaitreD(tables);

        var rs = reservedSeats
            .Where(s => s > 0)
            .Select(
                Some.Reservation.WithQuantity
            );

        Reservation r = Some.Reservation.WithQuantity(11);

        var willAccept = sut.WillAccept(rs, r);

        Assert.True(willAccept);
    }

    [SuppressMessage(
            "Performance",
            "CA1812: Avoid uninstantiated internal classes",
            Justification = "This class is instantiated via Reflection.")]
    private sealed class RejectTestCases :
    TheoryData<IEnumerable<Table>, IEnumerable<Reservation>>
    {
        public RejectTestCases()
        {
            Add(new[] {
                Table.Communal(6),
                Table.Communal(6)
            },
            Array.Empty<Reservation>()
            );

            Add(new[] {
                Table.Communal(10)

            },
            Array.Empty<Reservation>()
            );

            Add(new[] {
                Table.Communal(12)
            },
            new[] { Some.Reservation.WithQuantity(2) }
            );
        }
    }

    [Theory(DisplayName = "Reject"), ClassData(typeof(RejectTestCases))]
    public void Reject(
        IEnumerable<Table> tables,
        IEnumerable<Reservation> reservations
    )
    {
        var sut = new MaitreD(
            tables
        );

        var r = Some.Reservation.WithQuantity(11);
        var actual = sut.WillAccept(reservations, r);

        Assert.False(actual);
    }

}
