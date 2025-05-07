using TECHCOOL.UI;

public class SalesOrderList : Screen
{
    public override string Title { get; set; } = "Sales Orders";

    void ShowInfo(SalesOrder so)
    {
        Screen.Display(new SalesInfo());
    }

    protected override void Draw()
    {
        ListPage<SalesOrder> lp = new ListPage<SalesOrder>();
        lp.AddColumn("Order ID", nameof(SalesOrder.OrderId));
        lp.AddColumn("Date Created", nameof(SalesOrder.Created));
        lp.AddColumn("Customer ID", nameof(SalesOrder.CustomerId));
        lp.AddColumn("Customer Name", nameof(Customer.GetFullName));
        lp.AddColumn("Price", nameof(SalesOrder.TotalPrice));

        lp.Add(Database.Instance?.GetSales());
        
        lp.AddKey(ConsoleKey.F1, ShowInfo);                
        lp.Select();   

    }
}