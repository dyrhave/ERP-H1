using TECHCOOL.UI;

public class SalesInfo : Screen
{
    public override string Title { get; set; } = "Sales Order";

    void Back()
    {
        Quit();
    }

    protected override void Draw()
    {
        ListPage<SalesOrder> lp = new ListPage<SalesOrder>();
        lp.AddColumn("Order ID", nameof(SalesOrder.OrderId));
        lp.AddColumn("Sales Date", nameof(SalesOrder.Created));
        lp.AddColumn("Customer ID", nameof(SalesOrder.CustomerId));
        lp.AddColumn("Customer Name", nameof(Customer.GetFullName));
    }
}