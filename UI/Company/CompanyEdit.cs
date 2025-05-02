using System.Data.Entity.Infrastructure.MappingViews;
using TECHCOOL.UI;

public class CompanyEdit : Screen
{
    
    public override string Title {get; set;} = "Edit Company";
    void Back(Company _)
    {        
        Quit();        
    }

    protected override void Draw()
    {
        Company company = new Company();

        Form<Company> Fm = new();
        Fm.TextBox("Title", nameof(Company.Name));
        
        Fm.TextBox("Street", nameof(Company.Street));
        
        Fm.TextBox("StreetNumber", nameof(Company.StreetNumber));
        
        Fm.TextBox("PostalCode", nameof(Company.PostCode));
        
        Fm.TextBox("City", nameof(Company.City));
        
        Fm.TextBox("Contry", nameof(Company.Contry));
        
        Fm.SelectBox("Currency", nameof(Company.Currency));
        Fm.AddOption("Currency","USD", Currency.USD);
        Fm.AddOption("Currency", "EUR", Currency.EUR);
        Fm.AddOption("Currency", "DKK", Currency.DKK);
        Fm.AddOption("Currency", "SEK", Currency.SEK);
        Fm.Edit(company);
        
       // Screen.AddKey(ConsoleKey.Escape, Back);


        
    }
}