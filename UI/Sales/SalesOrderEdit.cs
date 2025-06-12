using TECHCOOL.UI;

public class SalesOrderEdit : Screen
{
    SalesOrderHeader _so;

    public SalesOrderEdit(SalesOrderHeader so)
    {
        _so = so;
    }
    public override string Title { get; set; } = "Edit Sales Order";


    protected override void Draw()
    {
        SalesOrderHeader salesOrder = new();

        Form<SalesOrderHeader> editor = new();
        editor.TextBox("Customer ID", nameof(SalesOrderHeader.CustomerId));
        editor.TextBox("Created", nameof(SalesOrderHeader.Created));
        editor.TextBox("Order Completed", nameof(SalesOrderHeader.OrderCompleted));
        editor.TextBox("State", nameof(SalesOrderHeader.State));
        
        if (editor.Edit(_so))
        {
            try
            {
                Database.Instance.UpdateSalesOrder(_so);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating sales order: {ex.Message}");
                Console.ReadKey();
                return;
            }
        }
        Quit();
    }
}