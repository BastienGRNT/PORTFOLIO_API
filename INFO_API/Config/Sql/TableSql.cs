namespace API.Config.Sql;

public static class TableSql
{
    public static string CreateTableTest { get; } = "CREATE TABLE IF NOT EXISTS test (id SERIAL PRIMARY KEY);";
}