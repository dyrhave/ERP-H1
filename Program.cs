using TECHCOOL.UI;
using Microsoft.Data.SqlClient;


try 
{
    // Connect to the database
    var connection = Database.Instance.GetConnection();
    Console.WriteLine("Database connection successful!");

    // Initialize UI components
    CompanyListPage companylistpage = new();
    ProductListPage productlistpage = new();
    CustomerListPage customerlistPage = new();
    SalesOrderList salesOrderList = new();

    // Set up main menu
    Menu mainMenu = new();
    mainMenu.Add(companylistpage);
    mainMenu.Add(productlistpage);
    mainMenu.Add(customerlistPage);
    mainMenu.Add(salesOrderList);
    
    // Display the menu
    Screen.Display(mainMenu);
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
}