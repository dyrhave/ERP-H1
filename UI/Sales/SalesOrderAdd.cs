using System.ComponentModel;
using TECHCOOL.UI;

public class SalesOrderAdd : Screen
{
    public override string Title { get; set; } = "Add Sales Order";

    protected override void Draw()
    {
        SalesOrder so = new();

        Form<SalesOrder> edit = new Form<SalesOrder>();
        edit.TextBox("Customer ID", nameof(SalesOrder.CustomerId));
        edit.TextBox("Total Price", nameof(SalesOrder.TotalPrice));
        edit.TextBox("Created", nameof(SalesOrder.Created));

        if (edit.Edit(so))
        {
            try
            {
                Database.Instance.AddSale(so);
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