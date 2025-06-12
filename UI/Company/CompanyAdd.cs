using TECHCOOL.UI;
public class CompanyAdd : Screen
{
     public override string Title {get; set;} = "Add Company";
    protected override void Draw()
    {
        Company company = new Company();
        
        
        Form<Company> editor = new();
        editor.TextBox("Title", nameof(Company.Name));        
        editor.TextBox("Street", nameof(Address.Street));
        editor.TextBox("StreetNumber", nameof(Address.StreetNumber));        
        editor.TextBox("PostalCode", nameof(Address.PostCode));        
        editor.TextBox("City", nameof(Address.City));
        editor.TextBox("Contry", nameof(Address.Country));
        editor.SelectBox("Currency", nameof(Company.Currency));
        editor.AddOption("Currency","USD", Currency.USD);
        editor.AddOption("Currency", "EUR", Currency.EUR);
        editor.AddOption("Currency", "DKK", Currency.DKK);
        editor.AddOption("Currency", "SEK", Currency.SEK);
        
        if (editor.Edit(company))
        {
            Database.Instance.AddAddress(company.Address);
            company.AddressId = company.Address.AddressId;
            Database.Instance.AddCompany(company);
            Quit();
        }
    }

}