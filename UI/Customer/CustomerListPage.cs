using TECHCOOL.UI;

public class CustomerListPage : Screen
{
    public override string Title { get; set; } = "Customer";

    void ShowInfo(Customer _)
    {
        Display(new CustomerInfo());
    }
 
    void ShowAdd(Customer _)
    {
        Display(new CustomerAdd());
    }
   
    void Back(Customer _)
    {        
        Quit();       
    }

    protected override void Draw()
    {
        ListPage<Customer> lp = new();
        lp.AddColumn("CustomerId", nameof(Customer.CustomerId));
        lp.AddColumn("Name", nameof(Customer.FullName));
        lp.AddColumn("Phone", nameof(Customer.Phone));
        lp.AddColumn("Email", nameof(Customer.Email));

        lp.Add(Database.Instance?.GetCustomers());

        lp.AddKey(ConsoleKey.F1, ShowInfo);                
        lp.AddKey(ConsoleKey.Escape, Back);
        lp.AddKey(ConsoleKey.F3, ShowAdd);
        lp.Select();
    }
}