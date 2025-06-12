using System.ComponentModel;
using TECHCOOL.UI;

public class SalesOrderAdd : Screen
{
    public override string Title { get; set; } = "Add Sales Order";    protected override void Draw()
    {
        SalesOrderHeader so = new();
        // Default state for new orders
        so.State = SalesOrderState.Pending;

        Form<SalesOrderHeader> edit = new Form<SalesOrderHeader>();
        edit.TextBox("Customer ID", nameof(SalesOrderHeader.CustomerId));
        edit.SelectBox("State", nameof(SalesOrderHeader.State));
        edit.AddOption("State", "Pending", SalesOrderState.Pending);
        edit.AddOption("State", "Processing", SalesOrderState.Processing);
        edit.AddOption("State", "Completed", SalesOrderState.Completed);
        edit.AddOption("State", "Cancelled", SalesOrderState.Cancelled);

        if (edit.Edit(so))
        {
            try
            {
                Database.Instance.AddSalesOrder(so);
                Console.WriteLine($"Sales order created successfully with ID: {so.OrderId}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding sales order: {ex.Message}");
                Console.ReadKey();
                return;
            }
        }
        Quit();
    }
}