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
        add.TextBox("Street", nameof(Customer.Street));
        add.TextBox("Street Number", nameof(Customer.StreetNumber));
        add.TextBox("City", nameof(Customer.City));
        add.TextBox("PostCode", nameof(Customer.PostCode));
        add.TextBox("Country", nameof(Customer.Country));

        add.Edit(c);

        try
        {
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