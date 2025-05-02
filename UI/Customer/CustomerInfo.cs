using TECHCOOL.UI;

public class CustomerInfo : Screen
{
    public override string Title { get; set; } = "Customer";

    Void Back(Customer _)
    {
        Quit();
    }

    protected override void Draw()
    {
        ListPage<Customer> lp = new();
        lp.AddColumn("Name", nameof(Customer.GetFullName));
        lp.AddColumn("Address", nameof(Customer.CustomerFullAddress));
        lp.AddColumn("Latest Purchase", nameof(Customer.LastPurchaseDate));
    }
}
