using System.Data.Entity;

public partial class Database
{
    public static Database? Instance { get; set; }
    public Database()
    {
        Instance ??= this;
    }
}