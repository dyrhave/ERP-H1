using TECHCOOL.UI;

public class SalesOrderList : Screen
{
    public override string Title { get; set; } = "Sales Orders";

    void ShowInfo(SalesOrder so)
    {
        Display(new SalesInfo());
    }

    void showAdd(SalesOrder so)
    {
        Display(new SalesOrderAdd());
    }
    
    void Back(SalesOrder so)
    {
        Quit();
    }

    protected override void Draw()
    {
        ListPage<SalesOrder> lp = new();
        lp.AddColumn("Order ID", nameof(SalesOrder.OrderId));
        lp.AddColumn("Date Created", nameof(SalesOrder.Created));
        lp.AddColumn("Customer ID", nameof(SalesOrder.CustomerId));
        lp.AddColumn("Customer Name", nameof(Customer.FullName));
        lp.AddColumn("Price", nameof(SalesOrder.TotalPrice));

        lp.Add(Database.Instance?.GetSales());

        lp.AddKey(ConsoleKey.F1, ShowInfo);
        lp.AddKey(ConsoleKey.F2, showAdd);
        lp.AddKey(ConsoleKey.Escape, Back);
        lp.Select();


    }
}