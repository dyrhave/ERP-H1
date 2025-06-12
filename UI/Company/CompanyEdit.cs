using System.Data.Entity.Infrastructure.MappingViews;
using TECHCOOL.UI;

public class CompanyEdit : Screen
{
  Company _company;
  public CompanyEdit(Company company)
  {
    _company = company;
  }
  public override string Title { get; set; } = "Edit Company";


  protected override void Draw()
  {
    

    Form<Company> editor = new();
    editor.TextBox("Title", nameof(Company.Name));
    editor.TextBox("Street", nameof(Address.Street));
    editor.TextBox("StreetNumber", nameof(Address.StreetNumber));
    editor.TextBox("PostalCode", nameof(Address.PostCode));
    editor.TextBox("City", nameof(Address.City));
    editor.TextBox("Contry", nameof(Address.Country));
    editor.SelectBox("Currency", nameof(Company.Currency));
    editor.AddOption("Currency", "USD", Currency.USD);
    editor.AddOption("Currency", "EUR", Currency.EUR);
    editor.AddOption("Currency", "DKK", Currency.DKK);
    editor.AddOption("Currency", "SEK", Currency.SEK);




    if (editor.Edit(_company))
    {
       _company.Address.AddressId = _company.AddressId;
      Database.Instance.UpdateAddress(_company.Address);
      Database.Instance.UpdateCompany(_company);
      Quit();
    }

  }
}