using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace Infratructure.DataContext;

public interface IContext
{
    IDbConnection Connection();
}

public class DapperContext : IContext
{
    private readonly string connectionString =
        "Server=localhost; Port = 5432; Database = bank_dapper_db; Username = postgres; Password = 501040477.tj;";

    public IDbConnection Connection()
    {
        return new NpgsqlConnection(connectionString);
    }
}

public class SqlServerContext : IContext
{
    private readonly string connectionString =
        "Server=localhost; Port = 5432; Database = bank_dapper_db; Username = postgres; Password = 501040477.tj;";

    public IDbConnection  Connection()
    {
        return new SqlConnection(connectionString);
    }
}