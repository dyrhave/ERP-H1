public partial class Database
{
    private static Database? _instance;
    public static Database Instance => _instance ??= new Database();
    private Database()
    {
        // Connection string to db
    }
}