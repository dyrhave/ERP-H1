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

        Form<Company> editor = new();
        editor.TextBox("Title", nameof(Company.Name));        
        editor.TextBox("Street", nameof(Company.Street));        
        editor.TextBox("StreetNumber", nameof(Company.StreetNumber));        
        editor.TextBox("PostalCode", nameof(Company.PostCode));        
        editor.TextBox("City", nameof(Company.City));        
        editor.TextBox("Contry", nameof(Company.Contry));        
        editor.SelectBox("Currency", nameof(Company.Currency));
        editor.AddOption("Currency","USD", Currency.USD);
        editor.AddOption("Currency", "EUR", Currency.EUR);
        editor.AddOption("Currency", "DKK", Currency.DKK);
        editor.AddOption("Currency", "SEK", Currency.SEK);
        editor.Edit(company);
        
      // gemmer ikke og man kan ikke se det gamle ting inde i editoren

       
        Quit();
        
    }
}