using Npgsql;

namespace ConsoleApplication1diary;

public static class DatabaseRequests
{
    static string formattedDate = "";
    public static int clientId = 0;
    
    public static void CreateNewClient()
    {
        Console.WriteLine("Введите login ");
        string login = Console.ReadLine();
        Console.WriteLine("Введите password ");
        string password = Console.ReadLine();
        var querySql = $"INSERT INTO client (login, password) values ('{login}', '{password}')";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }
    
    public static void EnterClient()
    {
        Console.WriteLine("Введите login ");
        string login = Console.ReadLine();
        Console.WriteLine("Введите password ");
        string password = Console.ReadLine();
        var querySql = $"SELECT ID FROM client WHERE login = '{login}' AND password = '{password}'";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            clientId = int.Parse(reader[0].ToString());
        }

        if (clientId == 0)
        {
            Console.WriteLine("Неверный логин или пароль");
        }
    }
    
    public static void CreateNewTask()
    {
        Console.WriteLine("Введите заголовок ");
        string title = Console.ReadLine();
        Console.WriteLine("Введите задачу ");
        string desckription = Console.ReadLine();
        Console.WriteLine("Введите дату в формате гггг-мм-дд ");
        DateTime dateOfCompletion = new DateTime();
        try
        {
            dateOfCompletion = DateTime.Parse(Console.ReadLine());
            formattedDate = dateOfCompletion.ToString("yyyy-MM-dd HH:mm:ss");
        }
        catch
        {
            Console.WriteLine("Неверный формат даты");
            return;
        }
        
        var querySql =
            $"INSERT INTO diary(user_ID, Title, Desckription, date_of_completion) VALUES ({clientId}, '{title}', '{desckription}', '{formattedDate}')";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }

    public static void DeleteTask(int id)
    {
        var querySql = $"DELETE FROM diary WHERE ID = id";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }

    public static void UpdateTask(int id)
    {
        Console.WriteLine("Введите заголовок ");
        string title = Console.ReadLine();
        Console.WriteLine("Введите задачу ");
        string desckription = Console.ReadLine();
        Console.WriteLine("Введите дату в формате гггг-мм-дд ");
        DateTime dateOfCompletion = new DateTime();
        try
        {
            dateOfCompletion = DateTime.Parse(Console.ReadLine());
            formattedDate = dateOfCompletion.ToString("yyyy-MM-dd HH:mm:ss");
        }
        catch
        {
            Console.WriteLine("Неверный формат даты");
            return;
        }

        var querySql =
            $"UPDATE diary SET Title = '{title}', Desckription = '{desckription}', date_of_completion = '{formattedDate}' WHERE ID = {id} and user_id = {clientId}";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }

    public static void WriteTodayTasks()
    {
       
        formattedDate = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
            
        var querySql = $"SELECT * FROM diary where Date_of_completion = '{formattedDate}' and user_id = {clientId}";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(
                $"Id: {reader[0]} \nНазвание: {reader[2]} \nЗадача: {reader[3]} \nДата выполнения: {reader[4]}");
        }
    }

    public static void WriteTomorrowTasks()
    {
        formattedDate = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
        
        var querySql = $"SELECT * FROM diary where Date_of_completion = '{formattedDate}' and user_id = {clientId}";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(
                $"Id: {reader[0]} \nНазвание: {reader[2]} \nЗадача: {reader[3]} \nДата выполнения: {reader[4]}");
        }
    }

    public static void WriteWeekTasks()
    {
        int weekConvert = (int)DateTime.Today.DayOfWeek;
        if (weekConvert == 0)
        {
            weekConvert = 7;
        }

        formattedDate = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
        string formattedDateWeekConvert = DateTime.Today.AddDays(7 - weekConvert).ToString("yyyy-MM-dd HH:mm:ss");
        
        var querySql =
            $"SELECT * FROM diary where (Date_of_completion >= '{formattedDate}' AND Date_of_completion <= '{formattedDateWeekConvert}') and user_id = {clientId}";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(
                $"Id: {reader[0]} \nНазвание: {reader[2]} \nЗадача: {reader[3]} \nДата выполнения: {reader[4]}");
        }
    }

    public static void WriteAllTasks()
    {
        var querySql = $"SELECT * FROM diary where user_id = {clientId}";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(
                $"Id: {reader[0]} \nНазвание: {reader[2]} \nЗадача: {reader[3]} \nДата выполнения: {reader[4]}");
        }
    }

    public static void WriteUpcominTasks()
    {
        formattedDate = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
        
        var querySql = $"SELECT * FROM diary where Date_of_completion >= '{formattedDate}' and user_id = {clientId}";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(
                $"Id: {reader[0]} \nНазвание: {reader[2]} \nЗадача: {reader[3]} \nДата выполнения: {reader[4]}");
        }
    }

    public static void WritePastTasks()
    {
        formattedDate = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
        
        var querySql = $"SELECT * FROM diary where Date_of_completion <= '{formattedDate}' and user_id = {clientId}";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(
                $"Id: {reader[0]} \nНазвание: {reader[1]} \nЗадача: {reader[2]} \nДата выполнения: {reader[3]}");
        }
    }
}