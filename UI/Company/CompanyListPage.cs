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
    void ShowEdit(Company _)
    {
        Screen.Display(new CompanyEdit());
    }
    void Delete(Company cmp)
    {
        Database.Instance.DeleteCompany(cmp.CompanyId);
        Console.Clear();      
    }


    protected override void Draw()
    {
        ListPage<Company> lp = new();
        lp.AddColumn("Name", nameof(Company.Name));
        lp.AddColumn("Country", nameof(Company.Contry));
        lp.AddColumn("Currency", nameof(Company.Currency));

        lp.Add(Database.Instance.GetCompany());
        

        lp.AddKey(ConsoleKey.F1, ShowInfo);
        lp.AddKey(ConsoleKey.F2, ShowEdit);
        lp.AddKey(ConsoleKey.F5, Delete);
        lp.Select();    

    }
}