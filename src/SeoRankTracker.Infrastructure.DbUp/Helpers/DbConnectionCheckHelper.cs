using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SeoRankTracker.Infrastructure.DbUp.Helpers;

public static class DbConnectionCheckHelper
{
    public static bool CheckDbUpAndRunning(string connectionString, int retryAmount, float intervalSeconds = 1)
    {
        SqlConnectionStringBuilder seoDbConnStrBuilder = new(connectionString);
        string seoDatabaseName = seoDbConnStrBuilder.InitialCatalog;
        
        SqlConnectionStringBuilder mainDbConnStrBuilder = new(connectionString)
        {
            InitialCatalog = "master", // Connect to master database to create the new database if it doesn't exist
        };
        var masterConnectionString = mainDbConnStrBuilder.ConnectionString;
        
        // ensure db retry logic
        for (int i = 0; i < retryAmount; i++)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(masterConnectionString);
                connection.Open();
                using SqlCommand command = new SqlCommand("SELECT 1", connection);
                command.ExecuteScalar();
                // initialize database
                EnsureDatabaseExists(masterConnectionString, seoDatabaseName);
                Console.WriteLine("Database is up and running.");
                break;
            }
            catch (Exception e)
            {
                if (i == retryAmount - 1) return false;
                Console.WriteLine($"Db connection failed: {e.Message}\n>> Trying again {retryAmount - i - 1} more times with {intervalSeconds} sec intervals.");
                Task.Delay(TimeSpan.FromSeconds(intervalSeconds)).Wait();
            }
        }
        return true;
    }
    
    private static void EnsureDatabaseExists(string masterConnectionString, string databaseName)
    {
        using var connection = new SqlConnection(masterConnectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = $@"
            IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{databaseName}')
            BEGIN
                CREATE DATABASE [{databaseName}]
            END";
        command.ExecuteNonQuery();
    }
}