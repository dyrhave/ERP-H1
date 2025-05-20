using Microsoft.Data.SqlClient;

public partial class Database
{
    private static Database? _instance;

    public static Database Instance => _instance ??= new Database();
    
    private Database()
    {
        // Singleton constructor
    }

    public SqlConnection GetConnection()
    {
        SqlConnectionStringBuilder sb = new();
        sb.DataSource = "localhost";
        sb.InitialCatalog = "database";
        sb.UserID = "bruger";
        sb.Password = "nogethemmeligt";

        string connectionString = sb.ConnectionString;
        return new SqlConnection(connectionString);
    }
}