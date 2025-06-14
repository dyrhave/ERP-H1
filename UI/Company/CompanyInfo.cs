using TECHCOOL.UI;
using System;

public class CompanyInfo : Screen
{
    public override string Title {get; set;} = "Company";
    
    void Delete(Company cmp)
    {
        Database.Instance.DeleteCompany(cmp.CompanyId);
        Database.Instance.DeleteAddress(cmp.AddressId);
        Console.Clear();      
    }
    void Back(Company _)
    {        
        Quit();       
    }
    void ShowEdit(Company Cmp)
    {
        Screen.Display(new CompanyEdit(Cmp));
    }
    

    protected override void Draw()
    {
       
        ListPage<Company> lp = new();
        lp.AddColumn("Name", nameof(Company.Name));
        lp.AddColumn("Street", nameof(Address.Street));
        lp.AddColumn("StreetNumber", nameof(Address.StreetNumber));
        lp.AddColumn("Postcode", nameof(Address.PostCode));
        lp.AddColumn("City", nameof(Address.City));
        lp.AddColumn("Country", nameof(Address.Country));
        lp.AddColumn("Currency", nameof(Company.Currency));


        
        lp.Add(Database.Instance?.GetCompany());
        lp.AddKey(ConsoleKey.F2, ShowEdit);        
        lp.AddKey(ConsoleKey.F5, Delete);
        lp.AddKey(ConsoleKey.Escape, Back);
        lp.Select();
    }
}