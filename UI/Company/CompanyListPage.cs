using System.Formats.Tar;
using Mysqlx.Crud;
using TECHCOOL.UI;

public class CompanyListPage : Screen
{
    public override string Title { get; set; } = "Company";
    void ShowInfo(Company _)
    {
        Screen.Display(new CompanyInfo());
    }
    void ShowAdd(Company _)
    {
        Screen.Display(new CompanyAdd());
    }
    
    void Back(Company _)
    {        
        Quit();       
    }
    
    // void ShowAdd(Company _)


    protected override void Draw()
    {

        ListPage<Address> lpAddress = new();
        ListPage<Company> lp = new();
        lp.AddColumn("Name", nameof(Company.Name));
        lp.AddColumn("Country", nameof(Address.Country));
        lp.AddColumn("Currency", nameof(Company.Currency));

        lpAddress.Add(Database.Instance?.GetAddresses());
        lp.Add(Database.Instance.GetCompany());


        lp.AddKey(ConsoleKey.F1, ShowInfo);
        lp.AddKey(ConsoleKey.Escape, Back);
        lp.AddKey(ConsoleKey.F3, ShowAdd);
        lp.Select();


    }
}