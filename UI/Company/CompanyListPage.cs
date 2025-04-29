using TECHCOOL.UI;

public class CompanyListPage : Screen
{
    public override string Title {get; set;} = "Company";

    protected override void Draw()
    {
        ListPage<Company> lp = new();
        lp.AddColumn("Name", nameof(Company.Name));

        lp.Add(Database.Instance.GetCompany());

        lp.Select();
    }
}