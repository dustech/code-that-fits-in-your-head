using Dustech.Restaurant.RestApi.Interfaces; // IReservationsRepository
using Dustech.Restaurant.RestApi.Models; // Reservation
using Microsoft.Data.SqlClient; // SqlConnection,SqlCommand,SqlParameter
using System.Data; // SqlDbType

namespace Dustech.Restaurant.RestApi.Repositories;

public class SqlReservationsRepository(string connectionString) : IReservationsRepository
{
    public string ConnectionString { get; }
    = connectionString ?? throw new ArgumentNullException(connectionString, nameof(connectionString));

    public async Task Create(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);

        using SqlConnection conn = new(ConnectionString);
        // using SqlCommand cmd = new (createReservationSql, conn);
        using SqlCommand cmd = new(createReservationSql, conn);
        SqlParameter p1, p2, p3, p4;
        p1 = new()
        {
            ParameterName = "At",
            SqlDbType = SqlDbType.DateTime,
            SqlValue = reservation.At
        };
        p2 = new()
        {
            ParameterName = "Name",
            SqlDbType = SqlDbType.NVarChar,
            SqlValue = reservation.Name
        };
        p3 = new()
        {
            ParameterName = "Email",
            SqlDbType = SqlDbType.NVarChar,
            SqlValue = reservation.Email
        };
        p4 = new()
        {
            ParameterName = "Quantity",
            SqlDbType = SqlDbType.Int,
            SqlValue = reservation.Quantity
        };
        cmd.Parameters.AddRange([p1, p2, p3, p4]);
        // cmd.Parameters.Add(new SqlParameter("@At", reservation.At));
        // cmd.Parameters.Add(new SqlParameter("@Name", reservation.Name));
        // cmd.Parameters.Add(new SqlParameter("@Email", reservation.Email));
        // cmd.Parameters.Add(
        //     new SqlParameter("@Quantity", reservation.Quantity));

        await conn.OpenAsync().ConfigureAwait(false);
        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
    }

    public async Task<IReadOnlyCollection<Reservation>> ReadReservations(
            DateTime dateTime)
    {
        var result = new List<Reservation>();

        using SqlConnection conn = new(ConnectionString);
        using SqlCommand cmd = new(readByRangeSql, conn);
        SqlParameter p1;
        p1 = new()
        {
            ParameterName = "At",
            SqlDbType = SqlDbType.DateTime,
            SqlValue = dateTime.Date
        };
        cmd.Parameters.AddRange([p1]);

        await conn.OpenAsync().ConfigureAwait(false);
        using var rdr =
            await cmd.ExecuteReaderAsync().ConfigureAwait(false);

        while (await rdr.ReadAsync().ConfigureAwait(false))
            result.Add(
                new Reservation(
                    (DateTime)rdr["At"],
                    (string)rdr["Name"],
                    (string)rdr["Email"],
                    (int)rdr["Quantity"]));

        return result.AsReadOnly();
    }

    private const string createReservationSql = @"
            INSERT INTO
                [dbo].[Reservations] ([At], [Name], [Email], [Quantity])
            VALUES (@At, @Name, @Email, @Quantity)";

    private const string readByRangeSql = @"
            SELECT [At], [Name], [Email], [Quantity]
            FROM [dbo].[Reservations]
            WHERE CONVERT(DATE, [At]) = @At";
}
