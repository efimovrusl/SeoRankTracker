using DbUp;
using DbUp.Helpers;
using SeoRankTracker.Infrastructure.DbUp.Helpers;

namespace SeoRankTracker.Infrastructure.DbUp.Infrastructure;

public static class DatabaseInitializer
{
    public static void ApplyMigrations(string connectionString)
    {
        if (!DbConnectionCheckHelper.CheckDbUpAndRunning(connectionString, 10, 5))
            throw new Exception("Couldn't connect to the database. Check Docker containers.");

        DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsFromFileSystem("./Migrations/Incremental")
            .WithTransactionPerScript()
            .WithExecutionTimeout(TimeSpan.FromSeconds(60))
            .JournalToSqlTable("dbo", "_SchemaVersions")
            .LogToConsole()
            .Build()
            .PerformUpgrade();

        DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsFromFileSystem("./Migrations/Idempotent")
            .WithTransactionPerScript()
            .WithExecutionTimeout(TimeSpan.FromSeconds(60))
            .JournalTo(new NullJournal())
            .LogToConsole()
            .Build()
            .PerformUpgrade();
    }
}