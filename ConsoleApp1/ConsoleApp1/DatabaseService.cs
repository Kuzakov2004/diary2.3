using Npgsql;

namespace ConsoleApplication1diary;

public class DatabaseService
{
    private static NpgsqlConnection? _connection; 
private static string GetConnectionString()
    {
        return @"Host=10.30.0.137;Port=5432;Database=gr624_kudal;Username=gr624_kudal;Password=mamama-123";
    }

    /// <summary>    /// Метод GetSqlConnection()
    /// проверяет есть ли уже открытое соединение с БД    /// если нет, то открывает соединение с БД
    /// </summary>    /// <returns></returns>
    public static NpgsqlConnection GetSqlConnection()
    {
        if (_connection is null)
        {
            _connection = new NpgsqlConnection(GetConnectionString());
            _connection.Open();
        }

        return _connection;
    }
}