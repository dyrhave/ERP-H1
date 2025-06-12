using TECHCOOL.UI;

public class CustomerEdit : Screen
{
    public override string Title { get; set; } = "Edit Customer";
    Customer _customerToEdit;

    public CustomerEdit(Customer costumer)
    {
        _customerToEdit = costumer;
    }
    
    protected override void Draw()
    {
        Form<Customer> editor = new();
        editor.TextBox("First Name", nameof(Customer.FirstName));
        editor.TextBox("Last Name", nameof(Customer.LastName));
        editor.TextBox("Email", nameof(Customer.Email));
        editor.TextBox("Street", nameof(Address.Street));
        editor.TextBox("Street Number", nameof(Address.StreetNumber));
        editor.TextBox("City", nameof(Address.City));
        editor.TextBox("PostCode", nameof(Address.PostCode));
        editor.TextBox("Country", nameof(Address.Country));

        editor.Edit(_customerToEdit);

        try
        {
            Database.Instance.AddAddress(_customerToEdit.Address);
            Database.Instance.UpdateCustomer(_customerToEdit);
            Console.WriteLine($"Customer {_customerToEdit.FullName} updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating customer: {ex.Message}");
            Console.ReadKey();
            return;
        }

        Quit();
    }
}