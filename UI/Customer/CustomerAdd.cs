using System.Data.SQLite;
using System.Linq.Expressions;
using TECHCOOL.UI;

public class CustomerAdd : Screen
{
    public override string Title { get; set; } = "Add Customer";
    
    protected override void Draw()
    {
        Customer c = new();
        
        Form<Customer> add = new();
        add.TextBox("First Name", nameof(Customer.FirstName));
        add.TextBox("Last Name", nameof(Customer.LastName));
        add.TextBox("Email", nameof(Customer.Email));
        add.TextBox("Street", nameof(Address.Street));
        add.TextBox("Street Number", nameof(Address.StreetNumber));
        add.TextBox("City", nameof(Address.City));
        add.TextBox("PostCode", nameof(Address.PostCode));
        add.TextBox("Country", nameof(Address.Country));

        add.Edit(c);

        try
        {
            Database.Instance.AddAddress(c.Address);
            c.AddressId = c.Address.AddressId;
            Database.Instance.AddCustomer(c);
            Console.WriteLine($"Customer {c.FullName} added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding customer: {ex.Message}");
            Console.ReadKey();
            return;
        }

        Quit();
    }
}