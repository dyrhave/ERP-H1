using Microsoft.Data.SqlClient;

public partial class Database
{
    private static Database? _instance;

    public static Database Instance => _instance ??= new Database();
    
    private Database()
    {
        // Singleton constructor
    }

    public SqlConnection GetConnection() // Temporary structure - fix when database is implemented
    {
        SqlConnectionStringBuilder sb = new();
        sb.DataSource = "localhost";
        sb.InitialCatalog = "LNE_Security_ERP";
        sb.UserID = "remo";
        sb.Password = "Test123";
        sb.Encrypt = true;
        sb.TrustServerCertificate = true;

        string connectionString = sb.ConnectionString;
        return new SqlConnection(connectionString);
    }
}