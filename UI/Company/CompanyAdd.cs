using TECHCOOL.UI;
public class CompanyAdd : Screen
{
    public override string Title {get; set;} = "Add Company";
    protected override void Draw()
    {
        Company company = new Company();
        
        Form<Company> editor = new();
        editor.TextBox("Title", nameof(Company.Name));        
        editor.TextBox("Street", nameof(Company.Street));
        editor.TextBox("StreetNumber", nameof(Company.StreetNumber));        
        editor.TextBox("PostalCode", nameof(Company.PostCode));        
        editor.TextBox("City", nameof(Company.City));
        editor.TextBox("Contry", nameof(Company.Country));        
        editor.SelectBox("Currency", nameof(Company.Currency));
        editor.AddOption("Currency","USD", Currency.USD);
        editor.AddOption("Currency", "EUR", Currency.EUR);
        editor.AddOption("Currency", "DKK", Currency.DKK);
        editor.AddOption("Currency", "SEK", Currency.SEK);
        
        if (editor.Edit(company))
        {
            Database.Instance.AddCompany(company);
            Quit();
        }
    }

}