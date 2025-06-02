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
    // void ShowEdit(Customer _)
    // {
    //     Display(new CustomerEdit(Customer selectedCustomer);
    // }
    


    protected override void Draw()
    {
        Clear();

        ListPage<Customer> lp = new();
        lp.AddColumn("Name", nameof(Customer.GetFullName));
        lp.AddColumn("Address", nameof(Customer.CustomerFullAddress));
        lp.AddColumn("Latest Purchase", nameof(Customer.LastPurchaseDate));

        lp.AddKey(ConsoleKey.Escape, Back);
        lp.AddKey(ConsoleKey.F5, Delete);
        // lp.AddKey(ConsoleKey.F2, ShowEdit);
    }
}
