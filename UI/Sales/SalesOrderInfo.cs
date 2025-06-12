using TECHCOOL.UI;

public class SalesInfo : Screen
{
    public override string Title { get; set; } = "Sales Order";

    void Back(SalesOrderHeader so) => Quit();

    void Delete(SalesOrderHeader so)
    {
        Database.Instance?.DeleteSalesOrder(so.OrderId);
        Clear();
    }
    void ShowEdit(SalesOrderHeader so)
    {
        Display(new SalesOrderEdit(so));
    }
    

    protected override void Draw()
    {
        ListPage<SalesOrderHeader> lp = new();
        lp.AddColumn("Order ID", nameof(SalesOrderHeader.OrderId));
        lp.AddColumn("Sales Date", nameof(SalesOrderHeader.Created));
        lp.AddColumn("Customer ID", nameof(SalesOrderHeader.CustomerId));
        lp.AddColumn("Customer Name", nameof(SalesOrderHeader.FullName));
        lp.AddColumn("State", nameof(SalesOrderHeader.State));

        lp.Add(Database.Instance?.GetSalesOrders());

        lp.AddKey(ConsoleKey.Escape, Back);
        lp.AddKey(ConsoleKey.F5, Delete);
        lp.AddKey(ConsoleKey.F2, ShowEdit);
        lp.Select();
    }
}