using TECHCOOL.UI;

public class CustomerInfo : Screen
{
    public override string Title { get; set; } = "Customer";

    void Back(Customer _)
    {
        Quit();
    }
    void Delete(Customer cust)
    {
        Database.Instance?.DeleteCustomer(cust.CustomerId);
        Console.Clear();      
    }
    void ShowEdit(Customer customer)
    {
        Display(new CustomerEdit(customer));
    }


    protected override void Draw()
    {
        ListPage<Customer> lp = new();
        lp.AddColumn("Name", nameof(Customer.FullName));
        lp.AddColumn("Address", nameof(Customer.FullAddress));
        lp.AddColumn("Street", nameof(Address.Street));
        lp.AddColumn("StreetNumber", nameof(Address.StreetNumber));
        lp.AddColumn("Postcode", nameof(Address.PostCode));
        lp.AddColumn("City", nameof(Address.City));
        lp.AddColumn("Country", nameof(Address.Country));
        lp.AddColumn("Latest Purchase", nameof(Customer.LastPurchaseDate));

        lp.Add(Database.Instance?.GetCustomers());

        lp.AddKey(ConsoleKey.Escape, Back);
        lp.AddKey(ConsoleKey.F5, Delete);
        lp.AddKey(ConsoleKey.F2, ShowEdit);
        lp.Select();
    }
}
