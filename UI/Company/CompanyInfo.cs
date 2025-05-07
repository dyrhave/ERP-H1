using TECHCOOL.UI;
using System;

public class CompanyInfo : Screen
{
    public override string Title {get; set;} = "Company";
    
    void Delete(Company cmp)
    {
        Database.Instance.DeleteCompany(cmp.CompanyId);
        Console.Clear();      
    }
    void Back(Company _)
    {        
        Quit();       
    }
    void ShowEdit(Company _)
    {
        Screen.Display(new CompanyEdit());
    }

    protected override void Draw()
    {
        
        ListPage<Company> lp = new();
        lp.AddColumn("Name", nameof(Company.Name));
        lp.AddColumn("Street", nameof(Company.Street));
        lp.AddColumn("StreetNumber", nameof(Company.StreetNumber));
        lp.AddColumn("Postcode", nameof(Company.PostCode));
        lp.AddColumn("City", nameof(Company.City));
        lp.AddColumn("Country", nameof(Company.Contry));
        lp.AddColumn("Currency", nameof(Company.Currency));
        
        

        lp.Add(Database.Instance?.GetCompany());
        lp.AddKey(ConsoleKey.F2, ShowEdit);
        lp.AddKey(ConsoleKey.F5, Delete);
        lp.AddKey(ConsoleKey.Escape, Back);
        lp.Select();
    }
}