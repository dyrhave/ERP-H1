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
        lp.AddColumn("Name", nameof(Person.FullName));
        lp.AddColumn("Phone", nameof(Person.Phone));
        lp.AddColumn("Email", nameof(Person.Email));

        lp.Add(Database.Instance?.GetCustomers());

        lp.AddKey(ConsoleKey.F1, ShowInfo);                
        lp.AddKey(ConsoleKey.Escape, Back);
        lp.AddKey(ConsoleKey.F3, ShowAdd);
        lp.Select();
    }
}