using Microsoft.Data.SqlClient;

public partial class Database
{
    private static Database? _instance;

    public static Database Instance => _instance ??= new Database();

    private Database()
    {
        try
        {
            using SqlConnection connection = GetConnection();
            connection.Open();
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Database connection failed: " + ex.Message);
        }
    }

    public SqlConnection GetConnection() // Temporary structure - fix when database is implemented
    {
        SqlConnectionStringBuilder sb = new();
        sb.DataSource = "DESKTOP-0PBVOB5";
        sb.InitialCatalog = "LNE_Security_ERP";
        sb.UserID = "GruppeMR";
        sb.Password = "Pass123";
        sb.Encrypt = true;
        sb.TrustServerCertificate = true;

        string connectionString = sb.ConnectionString;
        return new SqlConnection(connectionString);
    }
}