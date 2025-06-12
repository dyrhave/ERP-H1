using Microsoft.Data.SqlClient;

public partial class Database
{
    List<Address> addresses = new();
    public Address? GetAddressesById(int id)
    {
        foreach (var address in addresses)
        {
            if (address.AddressId == id)
            {
                return address;
            }
        }
        return null;

    }
    public Address[] GetAddresses() // Temporary structure - fix when database is implemented
    {
        List<Address> addressList = new();
        SqlConnection connection = GetConnection();


        string queryString = "SELECT * FROM AddressDatabase";
        using (SqlCommand command = new(queryString, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Address address = new()
                    {
                        AddressId = reader.GetInt32(0),
                        Street = reader.GetString(1),
                        StreetNumber = reader.GetString(2),
                        City = reader.GetString(3),
                        Country = reader.GetString(4),
                        PostCode = reader.GetString(5),
                        
                    };
                    addressList.Add(address);
                }
            }
        }

        return addressList.ToArray();
    }

    public void AddAddress(Address address)
    {
        SqlConnection conn = GetConnection();
        
        string sql = @"
        INSERT INTO AddressDatabase ( Street, StreetNumber, City, Country, PostCode)
        VALUES ( @Street, @StreetNumber, @City, @Country, @PostCode);
        SELECT SCOPE_IDENTITY();
    ";

        using SqlCommand cmd = new SqlCommand(sql, conn);        
        cmd.Parameters.AddWithValue("@Street", address.Street);
        cmd.Parameters.AddWithValue("@StreetNumber", address.StreetNumber);
        cmd.Parameters.AddWithValue("@City", address.City);
        cmd.Parameters.AddWithValue("@Country", address.Country);        
        cmd.Parameters.AddWithValue("@PostCode", address.PostCode);




        object result = cmd.ExecuteScalar();
        if (result != null && int.TryParse(result.ToString(), out int newId))
        {
            address.AddressId = newId;
        }
    }
    public void UpdateAddress(Address address)
    {
        SqlConnection conn = GetConnection();
        

        string sql = @"
            UPDATE AddressDatabase
            SET  Street = @Street, StreetNumber = @StreetNumber, City = @City, Country = @Country, PostCode = @PostCode
            WHERE AddressId = @AddressId;
        ";

        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@AddressId", address.AddressId);        
        cmd.Parameters.AddWithValue("@Street", address.Street);
        cmd.Parameters.AddWithValue("@StreetNumber", address.StreetNumber);
        cmd.Parameters.AddWithValue("@City", address.City);
        cmd.Parameters.AddWithValue("@Country", address.Country);
        cmd.Parameters.AddWithValue("@PostCode", address.PostCode);

        cmd.ExecuteNonQuery();
    }
    public void DeleteAddress(int id)
    {
       SqlConnection conn = GetConnection();
        

        string sql = "DELETE FROM AddressDatabase WHERE AddressId = @AddressId";
        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@AddressId", id);

        cmd.ExecuteNonQuery();


        addresses.RemoveAll(c => c.AddressId == id);
    }
}