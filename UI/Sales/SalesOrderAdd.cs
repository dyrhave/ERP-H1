using System.ComponentModel;
using TECHCOOL.UI;

public class SalesOrderAdd : Screen
{
    public override string Title { get; set; } = "Add Sales Order";

    protected override void Draw()
    {
        SalesOrderHeader so = new();

        Form<SalesOrderHeader> edit = new Form<SalesOrderHeader>();
        edit.TextBox("Customer ID", nameof(SalesOrderHeader.CustomerId));
        edit.TextBox("Order Amount", nameof(SalesOrderHeader.OrderAmount));
        edit.TextBox("Created", nameof(SalesOrderHeader.Created));

        if (edit.Edit(so))
        {
            try
            {
                Database.Instance.AddSalesOrder(so);
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