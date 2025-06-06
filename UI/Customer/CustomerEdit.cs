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
        editor.TextBox("Street", nameof(Customer.Street));
        editor.TextBox("Street Number", nameof(Customer.StreetNumber));
        editor.TextBox("City", nameof(Customer.City));
        editor.TextBox("PostCode", nameof(Customer.PostCode));
        editor.TextBox("Country", nameof(Customer.Country));

        editor.Edit(_customerToEdit);

        try
        {
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