using TECHCOOL.UI;

public class CustomerInfo : Screen
{
    public override string Title { get; set; } = "Customer";

    void Back(Customer _)
    {
        Quit();
    }
    void Delete(Customer cmp)
    {
        Database.Instance?.DeleteProduct(cmp.CustomerId);
        Console.Clear();      
    }
    

    protected override void Draw()
    {
        ListPage<Customer> lp = new();
        lp.AddColumn("Name", nameof(Customer.GetFullName));
        lp.AddColumn("Address", nameof(Customer.CustomerFullAddress));
        lp.AddColumn("Latest Purchase", nameof(Customer.LastPurchaseDate));

        lp.AddKey(ConsoleKey.Escape, Back);
        lp.AddKey(ConsoleKey.F5, Delete);
    }
}
