using TECHCOOL.UI;

public class CustomerEdit : Screen
{
    public override string Title { get; set; } = "Edit Customer";
    protected override void Draw()
    {
        Clear();
        Customer c = new Customer
        {
            FirstName = "",
            LastName = ""
        };
        
        Form<Customer> editor = new Form<Customer>();
        editor.TextBox("First Name", nameof(Customer.FirstName));
        editor.TextBox("Last Name", nameof(Customer.LastName));
        editor.TextBox("Email", nameof(Customer.Email));
        editor.TextBox("Street", nameof(Customer.Street));
        editor.TextBox("Street Number", nameof(Customer.StreetNumber));
        editor.TextBox("City", nameof(Customer.City));
        editor.TextBox("PostCode", nameof(Customer.PostCode));
        editor.TextBox("Country", nameof(Customer.Country));
        editor.IntBox("CustomerId", nameof(Customer.CustomerId));
    }
}