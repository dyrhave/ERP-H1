using Microsoft.Data.SqlClient;

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
    public Company[] GetCompany() // Temporary structure - fix when database is implemented
    {
        List<Company> companyList = new();
        using (SqlConnection connection = GetConnection())
        {
            connection.Open();
            string queryString = "SELECT * FROM CompanyDatabase";
            using (SqlCommand command = new(queryString, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Company company = new()
                        {
                            CompanyId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Street = reader.GetString(2),
                            StreetNumber = reader.GetString(3),
                            City = reader.GetString(4),
                            Country = reader.GetString(5),
                            PostCode = reader.GetString(6),
                            Currency = (Currency)Enum.Parse(typeof(Currency), reader.GetString(7))
                        };
                        companyList.Add(company);
                    }
                }
            }
        }
        return companyList.ToArray();
    }

    public void AddCompany(Company company)
{
    using SqlConnection conn = GetConnection();
    conn.Open();

    string sql = @"
        INSERT INTO CompanyDatabase (Name, Street, StreetNumber, City, Country, Currency, PostCode)
        VALUES (@Name, @Street, @StreetNumber, @City, @Country, @Currency, @PostCode);
        SELECT SCOPE_IDENTITY();
    ";

    using SqlCommand cmd = new SqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@Name", company.Name);
    cmd.Parameters.AddWithValue("@Street", company.Street);
    cmd.Parameters.AddWithValue("@StreetNumber", company.StreetNumber);
    cmd.Parameters.AddWithValue("@City", company.City);
    cmd.Parameters.AddWithValue("@Country", company.Country); 
    cmd.Parameters.AddWithValue("@Currency", company.Currency.ToString()); 
    cmd.Parameters.AddWithValue("@PostCode", company.PostCode);

  
    object result = cmd.ExecuteScalar();
    if (result != null && int.TryParse(result.ToString(), out int newId))
    {
        company.CompanyId = newId;
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
        oldCompany.Country = company.Country;
    }
    public void DeleteCompany(int id)
    {
        Company? company = GetCompanyById(id);
        if (company != null)
        {
            companies.Remove(company);
        }
    }
}