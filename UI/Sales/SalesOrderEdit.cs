using TECHCOOL.UI;

public class SalesOrderEdit : Screen
{
    public override string Title { get; set; } = "Edit Sales Order";

    void Back(SalesOrder so) => Quit();

    protected override void Draw()
    {
        SalesOrder salesOrder = new SalesOrder();

        Form<SalesOrder> editor = new();
        editor.TextBox("First Name", nameof(Customer.FirstName));
        editor.TextBox("Last Name", nameof(Customer.LastName));
        editor.TextBox("Street", nameof(Customer.Street));
        editor.TextBox("Street Number", nameof(Customer.StreetNumber));
        editor.TextBox("Postal Code", nameof(Customer.PostCode));
        editor.TextBox("City", nameof(Customer.City));
        editor.TextBox("Email", nameof(Customer.Email));
        editor.TextBox("Phone", nameof(Customer.Phone));
        editor.Edit(salesOrder);

        Quit();
    }
}