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
    
    
    void Back(Company _)
    {        
        Quit();       
    }


    protected override void Draw()
    {
        
       
        ListPage<Company> lp = new();
        lp.AddColumn("Name", nameof(Company.Name));
        lp.AddColumn("Country", nameof(Company.Country));
        lp.AddColumn("Currency", nameof(Company.Currency));

        lp.Add(Database.Instance.GetCompany());
        

        lp.AddKey(ConsoleKey.F1, ShowInfo);                
        lp.AddKey(ConsoleKey.Escape, Back);
        lp.Select();
        

    }
}