using TECHCOOL.UI;

public class CompanyEdit : Screen
{
    
    public override string Title {get; set;} = "Company";
    
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


        lp.Add(Database.Instance.GetCompany());
        
        lp.Select();
    }
}