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

    void PrintInvoice(SalesOrderHeader so)
    {
        InvoiceGenerator invoiceGenerator = new InvoiceGenerator(Database.Instance);
        string invoiceHtml = invoiceGenerator.GenerateInvoiceHtml(so.OrderId);
        
        string fileName = $"Invoice_Order_{so.OrderId}_{DateTime.Now:yyyyMMdd_HHmmss}.html";
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
        
        try
        {
            File.WriteAllText(filePath, invoiceHtml);
            Console.WriteLine($"Invoice saved to {filePath}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving invoice: {ex.Message}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
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
        lp.AddKey(ConsoleKey.F3, PrintInvoice);
        lp.Select();
    }
}