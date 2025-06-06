using TECHCOOL.UI;

public class SalesOrderEdit : Screen
{
    SalesOrder _so;

    public SalesOrderEdit(SalesOrder so)
    {
        _so = so;
    }
    public override string Title { get; set; } = "Edit Sales Order";


    protected override void Draw()
    {
        SalesOrder salesOrder = new();

        Form<SalesOrder> editor = new();
        editor.TextBox("First Name", nameof(Customer.FirstName));
        editor.TextBox("Last Name", nameof(Customer.LastName));
        editor.TextBox("Street", nameof(Customer.Street));
        editor.TextBox("Street Number", nameof(Customer.StreetNumber));
        editor.TextBox("Postal Code", nameof(Customer.PostCode));
        editor.TextBox("City", nameof(Customer.City));
        editor.TextBox("Email", nameof(Customer.Email));
        editor.TextBox("Phone", nameof(Customer.Phone));
        
        if (editor.Edit(_so))
        {
            try
            {
                Database.Instance.UpdateSale(_so);
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