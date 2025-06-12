using TECHCOOL.UI;

public class SalesOrderList : Screen
{
    public override string Title { get; set; } = "Sales Orders";

    void ShowInfo(SalesOrderHeader so)
    {
        Display(new SalesInfo());
    }

    void showAdd(SalesOrderHeader so)
    {
        Display(new SalesOrderAdd());
    }
    
    void Back(SalesOrderHeader so)
    {
        Quit();
    }

    protected override void Draw()
    {
        ListPage<SalesOrderHeader> lp = new();
        lp.AddColumn("Order ID", nameof(SalesOrderHeader.OrderId));
        lp.AddColumn("Date Created", nameof(SalesOrderHeader.Created));
        lp.AddColumn("Customer ID", nameof(SalesOrderHeader.CustomerId));
        lp.AddColumn("Customer Name", nameof(SalesOrderHeader.FullName));
        lp.AddColumn("State", nameof(SalesOrderHeader.State));
        lp.AddColumn("Price", nameof(SalesOrderHeader.OrderAmount));

        lp.Add(Database.Instance?.GetSalesOrders());

        lp.AddKey(ConsoleKey.F1, ShowInfo);
        lp.AddKey(ConsoleKey.F2, showAdd);
        lp.AddKey(ConsoleKey.Escape, Back);
        lp.Select();


    }
}