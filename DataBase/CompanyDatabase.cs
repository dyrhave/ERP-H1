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
    public Company[] GetCompany()
    {
        List<Company> companyList = new();
        SqlConnection connection = GetConnection();


        string queryString = @"SELECT CompanyId,Name,AddressDatabase.AddressId,Currency,Street,StreetNumber,City,Country,PostCode FROM CompanyDatabase
         LEFT JOIN AddressDatabase ON CompanyDatabase.AddressId = AddressDatabase.AddressId";
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
                        AddressId = reader.GetInt32(2),
                        Currency = (Currency)Enum.Parse(typeof(Currency), reader.GetString(3)),
                        Street = reader.GetString(4),
                        StreetNumber = reader.GetString(5),
                        City = reader.GetString(6),
                        Country = reader.GetString(7),
                        PostCode = reader.GetString(8)

                    };
                    companyList.Add(company);
                }
            }
        }

        return companyList.ToArray();
    }

    public void AddCompany(Company company)
    {
        SqlConnection conn = GetConnection();

        string sql = @"
        INSERT INTO CompanyDatabase (Name,AddressId, Currency)
        VALUES (@Name,@AddressId, @Currency);
        SELECT SCOPE_IDENTITY();
    ";

        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Name", company.Name);
        cmd.Parameters.AddWithValue("@AddressId", company.AddressId);
        cmd.Parameters.AddWithValue("@Currency", company.Currency.ToString());





        object result = cmd.ExecuteScalar();
        if (result != null && int.TryParse(result.ToString(), out int newId))
        {
            company.CompanyId = newId;
        }
    }
    public void UpdateCompany(Company company)
    {
        SqlConnection conn = GetConnection();


        string sql = @"
    UPDATE CompanyDatabase
    SET Name = @Name, Currency = @Currency, AddressId = @AddressId
    WHERE CompanyId = @CompanyId;
";

        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CompanyId", company.CompanyId);
        cmd.Parameters.AddWithValue("@Name", company.Name);
        cmd.Parameters.AddWithValue("@Currency", company.Currency.ToString());
        cmd.Parameters.AddWithValue("@AddressId", company.AddressId);


        cmd.ExecuteNonQuery();
    }
    public void DeleteCompany(int id)
    {
        SqlConnection conn = GetConnection();


        string sql = "DELETE FROM CompanyDatabase WHERE CompanyId = @CompanyId";
        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CompanyId", id);

        cmd.ExecuteNonQuery();


        companies.RemoveAll(c => c.CompanyId == id);
    }
}