using TECHCOOL.UI;

public class CustomerListPage : Screen
{
    public override string Title { get; set; } = "Customer";

    void ShowInfo(Customer _)
    {
        Screen.Display(new CustomerInfo());
    }

    void ShowEdit(Customer _)
    {
        Screen.Display(new CustomerEdit());
    }
   
     void Back(Customer _)
    {        
        Quit();       
    }

    protected override void Draw()
    {
        ListPage<Customer> lp = new();
        lp.AddColumn("CustomerId", nameof(Customer.CustomerId));
        lp.AddColumn("Name", nameof(Person.FirstName)+" "+nameof(Person.LastName));
        lp.AddColumn("Phone", nameof(Person.Phone));
        lp.AddColumn("Email", nameof(Person.Email));

        lp.Add(Database.Instance?.GetCustomers());

        lp.AddKey(ConsoleKey.F1, ShowInfo);
        lp.AddKey(ConsoleKey.F2, ShowEdit);        
        lp.AddKey(ConsoleKey.Escape, Back);
        lp.Select();
    }
}