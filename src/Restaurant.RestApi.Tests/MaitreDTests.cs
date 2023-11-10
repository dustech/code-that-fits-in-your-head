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
        var tables = tableSeats.Select(s => new Table(TableType.Communal, s));
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
    private sealed class RejectTestCases : TheoryData<IEnumerable<Table>>
    {
        public RejectTestCases()
        {
            Add(new[] {
                new Table(TableType:TableType.Communal, 6),
                new Table(TableType:TableType.Communal, 6)
            }

            );
        }
    }

    [Theory, ClassData(typeof(RejectTestCases))]
    public void Reject(IEnumerable<Table> tables)
    {
        var sut = new MaitreD(
            tables
        );

        Reservation r = Some.Reservation.WithQuantity(11);

        var actual = sut.WillAccept(Array.Empty<Reservation>(), r);

        Assert.False(actual);
    }

}
