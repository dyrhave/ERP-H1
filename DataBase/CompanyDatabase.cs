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

        string queryString = @"
            SELECT c.CompanyId, c.Name, c.AddressId, c.Currency, 
                   a.Street, a.StreetNumber, a.City, a.Country, a.PostCode
            FROM CompanyDatabase c
            LEFT JOIN AddressDatabase a ON c.AddressId = a.AddressId";
            
        using (SqlCommand command = new(queryString, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Company company = new()
                    {
                        CompanyId = reader.GetInt32(reader.GetOrdinal("CompanyId")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Street = reader.IsDBNull(reader.GetOrdinal("Street")) ? string.Empty : reader.GetString(reader.GetOrdinal("Street")),
                        StreetNumber = reader.IsDBNull(reader.GetOrdinal("StreetNumber")) ? string.Empty : reader.GetString(reader.GetOrdinal("StreetNumber")),
                        City = reader.IsDBNull(reader.GetOrdinal("City")) ? string.Empty : reader.GetString(reader.GetOrdinal("City")),
                        Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? string.Empty : reader.GetString(reader.GetOrdinal("Country")),
                        PostCode = reader.IsDBNull(reader.GetOrdinal("PostCode")) ? string.Empty : reader.GetString(reader.GetOrdinal("PostCode")),
                        Currency = (Currency)Enum.Parse(typeof(Currency), reader.GetString(reader.GetOrdinal("Currency")))
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

        // Insert address first
        string addressSql = @"
            INSERT INTO AddressDatabase (Street, StreetNumber, City, Country, PostCode)
            VALUES (@Street, @StreetNumber, @City, @Country, @PostCode);
            SELECT SCOPE_IDENTITY();";

        int addressId;
        using (SqlCommand addressCmd = new SqlCommand(addressSql, conn))
        {
            addressCmd.Parameters.AddWithValue("@Street", company.Street ?? (object)DBNull.Value);
            addressCmd.Parameters.AddWithValue("@StreetNumber", company.StreetNumber ?? (object)DBNull.Value);
            addressCmd.Parameters.AddWithValue("@City", company.City ?? (object)DBNull.Value);
            addressCmd.Parameters.AddWithValue("@Country", company.Country ?? (object)DBNull.Value);
            addressCmd.Parameters.AddWithValue("@PostCode", company.PostCode ?? (object)DBNull.Value);

            object result = addressCmd.ExecuteScalar();
            addressId = Convert.ToInt32(result);
        }

        // Then insert company with reference to address
        string companySql = @"
            INSERT INTO CompanyDatabase (Name, AddressId, Currency)
            VALUES (@Name, @AddressId, @Currency);
            SELECT SCOPE_IDENTITY();";

        using SqlCommand cmd = new SqlCommand(companySql, conn);
        cmd.Parameters.AddWithValue("@Name", company.Name);
        cmd.Parameters.AddWithValue("@AddressId", addressId);
        cmd.Parameters.AddWithValue("@Currency", company.Currency.ToString());

        object companyResult = cmd.ExecuteScalar();
        company.CompanyId = Convert.ToInt32(companyResult);
    }    public void UpdateCompany(Company company)
    {
        SqlConnection conn = GetConnection();
        
        // Get the current AddressId for this company
        string getAddressIdSql = "SELECT AddressId FROM CompanyDatabase WHERE CompanyId = @CompanyId";
        int addressId;
        
        using (SqlCommand getAddressCmd = new SqlCommand(getAddressIdSql, conn))
        {
            getAddressCmd.Parameters.AddWithValue("@CompanyId", company.CompanyId);
            object result = getAddressCmd.ExecuteScalar();
            addressId = Convert.ToInt32(result);
        }
        
        // Update the address
        string updateAddressSql = @"
            UPDATE AddressDatabase 
            SET Street = @Street, StreetNumber = @StreetNumber, City = @City, 
                Country = @Country, PostCode = @PostCode
            WHERE AddressId = @AddressId";
                
        using (SqlCommand updateAddressCmd = new SqlCommand(updateAddressSql, conn))
        {
            updateAddressCmd.Parameters.AddWithValue("@AddressId", addressId);
            updateAddressCmd.Parameters.AddWithValue("@Street", company.Street ?? (object)DBNull.Value);
            updateAddressCmd.Parameters.AddWithValue("@StreetNumber", company.StreetNumber ?? (object)DBNull.Value);
            updateAddressCmd.Parameters.AddWithValue("@City", company.City ?? (object)DBNull.Value);
            updateAddressCmd.Parameters.AddWithValue("@Country", company.Country ?? (object)DBNull.Value);
            updateAddressCmd.Parameters.AddWithValue("@PostCode", company.PostCode ?? (object)DBNull.Value);
            updateAddressCmd.ExecuteNonQuery();
        }

        // Update company record
        string updateCompanySql = @"
            UPDATE CompanyDatabase
            SET Name = @Name, Currency = @Currency
            WHERE CompanyId = @CompanyId";

        using SqlCommand cmd = new SqlCommand(updateCompanySql, conn);
        cmd.Parameters.AddWithValue("@CompanyId", company.CompanyId);
        cmd.Parameters.AddWithValue("@Name", company.Name);
        cmd.Parameters.AddWithValue("@Currency", company.Currency.ToString());

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