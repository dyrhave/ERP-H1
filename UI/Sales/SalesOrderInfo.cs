using TECHCOOL.UI;

public class SalesInfo : Screen
{
    public override string Title { get; set; } = "Sales Order";

    void Back(SalesOrder so) => Quit();
    void Delete(SalesOrder so)
    {
        Database.Instance?.DeleteSale(so.OrderId);
        Clear();
    }
    void ShowEdit(SalesOrder so)
    {
        Screen.Display(new SalesOrderEdit());
    }
    


    protected override void Draw()
    {
        ListPage<SalesOrder> lp = new();
        lp.AddColumn("Order ID", nameof(SalesOrder.OrderId));
        lp.AddColumn("Sales Date", nameof(SalesOrder.Created));
        lp.AddColumn("Customer ID", nameof(SalesOrder.CustomerId));
        lp.AddColumn("Customer Name", nameof(Customer.GetFullName));

        lp.Add(Database.Instance?.GetSales());
        lp.AddKey(ConsoleKey.Escape, Back);
        lp.AddKey(ConsoleKey.F2, (so) =>
        {
            SalesOrderEdit edit = new SalesOrderEdit();
        });

        lp.AddKey(ConsoleKey.Escape, Back);
        lp.AddKey(ConsoleKey.F5, Delete);
        lp.AddKey(ConsoleKey.F2, ShowEdit);
        lp.Select();
    }
}