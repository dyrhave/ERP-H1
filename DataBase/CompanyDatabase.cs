public partial class Database
{
    List<Company> companies = new();
    public Company? GetCompanyById(int id)
    {
        foreach (var company in companies)
        {
            if (company.CompanyId == id)
            {
                return company;
            }
        }
        return null;

    }
    public Company[] GetCompany()
    {
        return companies.ToArray();
    }

    public void AddCompany(Company company)
    {
        if (company.CompanyId == 0)
        {
            companies.Add(company);
            company.CompanyId = companies.Count;
        }
    }
    public void UpdateCompany(Company company)
    {
        if (company.CompanyId == 0)
        {
            return;
        }
        Company? oldCompany = GetCompanyById(company.CompanyId);
        if (oldCompany == null)
        {
            return;
        }
        oldCompany.Name = company.Name;
        oldCompany.Street = company.Street;
        oldCompany.StreetNumber = company.StreetNumber;
        oldCompany.City = company.City;
        oldCompany.Contry = company.Contry;
    }
    public void DeleteCompany(int id)
    {
        Company? company = GetCompanyById(id);
        if (company != null)
        companies.Remove(company);
    }
}