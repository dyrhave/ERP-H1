using Microsoft.Data.SqlClient;

public partial class Database
{
    private List<Customer> _customers = new List<Customer>();

    public Customer? GetCustomerById(int id)
    {
        foreach (Customer c in _customers)
        {
            if (c.CustomerId == id)
            {
                return c;
            }
        }
        return null;
    }

    public Customer[] GetCustomers() 
    {
        List<Customer> customerList = new();
        SqlConnection connection = GetConnection();
        
        string queryString = @"SELECT CustomerId,FirstName,LastName,Email,Phone,AddressDatabase.AddressId,Street,StreetNumber,City,Country,PostCode FROM CustomerDatabase
         LEFT JOIN AddressDatabase ON CompanyDatabase.AddressId = AddressDatabase.AddressId";
        using (SqlCommand command = new(queryString, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Customer customer = new()
                    {
                        CustomerId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                        Phone = reader.GetString(4),
                        AddressId = reader.GetInt32(2),                        
                        Street = reader.GetString(4),
                        StreetNumber = reader.GetString(5),
                        City = reader.GetString(6),
                        Country = reader.GetString(7),
                        PostCode = reader.GetString(8)                       
                    };
                    customerList.Add(customer);
                }
            }
        }
        return customerList.ToArray();
    }

    public void AddCustomer(Customer c)
    {
        SqlConnection connection = GetConnection();

        string queryString = "INSERT INTO CustomerDatabase (FirstName, LastName, Email, Phone) " +
                                "VALUES (@FirstName, @LastName,@AddressId, @Email, @Phone); " +
                                "SELECT SCOPE_IDENTITY();";

        using (SqlCommand command = new(queryString, connection))
        {
            command.Parameters.AddWithValue("@FirstName", c.FirstName);
            command.Parameters.AddWithValue("@LastName", c.LastName);
            command.Parameters.AddWithValue("@Email", c.Email);
            command.Parameters.AddWithValue("@AddressId", c.AddressId);
            command.Parameters.AddWithValue("@Phone", c.Phone);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error adding customer: {ex.Message}");
                throw;
            }
        }
    }

    public void UpdateCustomer(Customer c)
    {
        SqlConnection connection = GetConnection();

        string queryString = "UPDATE CustomerDatabase SET FirstName = @FirstName, LastName = @LastName, Email = @Email, " +
                             "Phone = @Phone,AddressId = @AddressId,  " +
                             " WHERE CustomerId = @CustomerId";
        using (SqlCommand command = new(queryString, connection))
        {
            command.Parameters.AddWithValue("@CustomerId", c.CustomerId);
            command.Parameters.AddWithValue("@FirstName", c.FirstName);
            command.Parameters.AddWithValue("@LastName", c.LastName);
            command.Parameters.AddWithValue("@Email", c.Email);
            command.Parameters.AddWithValue("@Phone", c.Phone);
            command.Parameters.AddWithValue("@AddressId", c.AddressId);


            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error updating customer: {ex.Message}");
                throw;
            }
        }
    }

    public void DeleteCustomer(int id)
    {
        SqlConnection connection = GetConnection();

        string queryString = "DELETE FROM CustomerDatabase WHERE CustomerId = @CustomerId";
        using (SqlCommand command = new(queryString, connection))
        {
            command.Parameters.AddWithValue("@CustomerId", id);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error deleting customer: {ex.Message}");
                throw;
            }
        }
        _customers.RemoveAll(c => c.CustomerId == id);
    }
}