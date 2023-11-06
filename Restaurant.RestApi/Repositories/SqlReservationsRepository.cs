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
        SqlParameter p1, p2, p3;
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
            ParameterName = "Name",
            SqlDbType = SqlDbType.NVarChar,
            SqlValue = reservation.Email
        };
        cmd.Parameters.AddRange([p1, p2, p3]);
        // cmd.Parameters.Add(new SqlParameter("@At", reservation.At));
        // cmd.Parameters.Add(new SqlParameter("@Name", reservation.Name));
        // cmd.Parameters.Add(new SqlParameter("@Email", reservation.Email));
        // cmd.Parameters.Add(
        //     new SqlParameter("@Quantity", reservation.Quantity));

        await conn.OpenAsync().ConfigureAwait(false);
        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
    }
    private const string createReservationSql = @"
            INSERT INTO
                [dbo].[Reservations] ([At], [Name], [Email], [Quantity])
            VALUES (@At, @Name, @Email, @Quantity)";
}
