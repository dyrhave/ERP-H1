using TECHCOOL.UI;

public class SalesOrderEdit : Screen
{
    SalesOrderHeader _so;

    public SalesOrderEdit(SalesOrderHeader so)
    {
        _so = so;
    }
    public override string Title { get; set; } = "Edit Sales Order";    protected override void Draw()
    {
        Form<SalesOrderHeader> editor = new();
        
        // Show read-only information
        Console.WriteLine($"Editing Sales Order ID: {_so.OrderId}");
        Console.WriteLine($"Created Date: {_so.Created}");
        Console.WriteLine($"Current Order Amount: {_so.OrderAmount:C}");
        if (!string.IsNullOrEmpty(_so.OrderCompleted))
        {
            Console.WriteLine($"Completed Date: {_so.OrderCompleted} at {_so.OrderCompletedTime}");
        }
        Console.WriteLine();
        
        // Editable fields
        editor.TextBox("Customer ID", nameof(SalesOrderHeader.CustomerId));
        editor.SelectBox("State", nameof(SalesOrderHeader.State));
        editor.AddOption("State", "Pending", SalesOrderState.Pending);
        editor.AddOption("State", "Processing", SalesOrderState.Processing);
        editor.AddOption("State", "Completed", SalesOrderState.Completed);
        editor.AddOption("State", "Cancelled", SalesOrderState.Cancelled);
        
        if (editor.Edit(_so))
        {
            try
            {
                Database.Instance.UpdateSalesOrder(_so);
                Console.WriteLine("Sales order updated successfully!");
                if (_so.State == SalesOrderState.Completed && !string.IsNullOrEmpty(_so.OrderCompleted))
                {
                    Console.WriteLine($"Order marked as completed on {_so.OrderCompleted} at {_so.OrderCompletedTime}");
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
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