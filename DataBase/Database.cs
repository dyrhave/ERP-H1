using Microsoft.Data.SqlClient;

public partial class Database
{
    private static Database? _instance;
    private readonly SqlConnection _connection;

    public static Database Instance => _instance ??= new Database();

    private Database()
    {
        SqlConnectionStringBuilder sb = new();
        sb.DataSource = "DESKTOP-0PBVOB5";
        sb.InitialCatalog = "LNE_Security_ERP";
        sb.UserID = "GruppeMR";
        sb.Password = "Pass123";
        sb.Encrypt = true;
        sb.TrustServerCertificate = true;

        _connection = new SqlConnection(sb.ConnectionString);

        try
        {
            _connection.Open();
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Database connection failed: " + ex.Message);
        }
    }

    public SqlConnection GetConnection() 
    {
        if (_connection.State == System.Data.ConnectionState.Closed)
        {
            try
            {
                _connection.Open();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Failed to open database connection: " + ex.Message);
                throw;
            }
        }
        else if (_connection.State == System.Data.ConnectionState.Broken)
        {
            try
            {
                _connection.Close();
                _connection.Open();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Failed to recover database connection: " + ex.Message);
                throw;
            }
        }
        return _connection;
    }
}