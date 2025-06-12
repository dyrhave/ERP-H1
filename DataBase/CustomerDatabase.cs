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

    public Customer? GetCustomerByIdWithPerson(int id)
    {
        SqlConnection connection = GetConnection();
        
        string queryString = @"
            SELECT c.CustomerId, c.LastPurchaseDate,
                   p.PersonId, p.FirstName, p.LastName, p.Email, p.Phone,
                   p.Street, p.StreetNumber, p.City, p.Country, p.PostCode
            FROM CustomerDatabase c
            INNER JOIN PersonDatabase p ON c.PersonId = p.PersonId
            WHERE c.CustomerId = @CustomerId";
            
        using (SqlCommand command = new(queryString, connection))
        {
            command.Parameters.AddWithValue("@CustomerId", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Customer()
                    {
                        CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                        Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? string.Empty : reader.GetString(reader.GetOrdinal("Phone")),
                        Street = reader.IsDBNull(reader.GetOrdinal("Street")) ? string.Empty : reader.GetString(reader.GetOrdinal("Street")),
                        StreetNumber = reader.IsDBNull(reader.GetOrdinal("StreetNumber")) ? string.Empty : reader.GetString(reader.GetOrdinal("StreetNumber")),
                        City = reader.IsDBNull(reader.GetOrdinal("City")) ? string.Empty : reader.GetString(reader.GetOrdinal("City")),
                        Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? string.Empty : reader.GetString(reader.GetOrdinal("Country")),
                        PostCode = reader.IsDBNull(reader.GetOrdinal("PostCode")) ? string.Empty : reader.GetString(reader.GetOrdinal("PostCode")),
                        LastPurchaseDate = reader.IsDBNull(reader.GetOrdinal("LastPurchaseDate")) ? DateTime.Now.ToString("dd-MM-yyyy") : reader.GetDateTime(reader.GetOrdinal("LastPurchaseDate")).ToString("dd-MM-yyyy")
                    };
                }
            }
        }
        return null;
    }

    public Customer[] GetCustomers() // Refactored to use proper JOINs with Person table
    {
        List<Customer> customerList = new();
        SqlConnection connection = GetConnection();
        
        string queryString = @"
            SELECT c.CustomerId, c.LastPurchaseDate,
                   p.PersonId, p.FirstName, p.LastName, p.Email, p.Phone,
                   p.Street, p.StreetNumber, p.City, p.Country, p.PostCode
            FROM CustomerDatabase c
            INNER JOIN PersonDatabase p ON c.PersonId = p.PersonId";
            
        using (SqlCommand command = new(queryString, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Customer customer = new()
                    {
                        CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                        Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? string.Empty : reader.GetString(reader.GetOrdinal("Phone")),
                        Street = reader.IsDBNull(reader.GetOrdinal("Street")) ? string.Empty : reader.GetString(reader.GetOrdinal("Street")),
                        StreetNumber = reader.IsDBNull(reader.GetOrdinal("StreetNumber")) ? string.Empty : reader.GetString(reader.GetOrdinal("StreetNumber")),
                        City = reader.IsDBNull(reader.GetOrdinal("City")) ? string.Empty : reader.GetString(reader.GetOrdinal("City")),
                        Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? string.Empty : reader.GetString(reader.GetOrdinal("Country")),
                        PostCode = reader.IsDBNull(reader.GetOrdinal("PostCode")) ? string.Empty : reader.GetString(reader.GetOrdinal("PostCode")),
                        LastPurchaseDate = reader.IsDBNull(reader.GetOrdinal("LastPurchaseDate")) ? DateTime.Now.ToString("dd-MM-yyyy") : reader.GetDateTime(reader.GetOrdinal("LastPurchaseDate")).ToString("dd-MM-yyyy")
                    };
                    customerList.Add(customer);
                }
            }
        }
        return customerList.ToArray();
    }    public void AddCustomer(Customer c)
    {
        SqlConnection connection = GetConnection();

        // First insert into PersonDatabase
        string personQueryString = @"
            INSERT INTO PersonDatabase (FirstName, LastName, Email, Phone, Street, StreetNumber, City, Country, PostCode) 
            VALUES (@FirstName, @LastName, @Email, @Phone, @Street, @StreetNumber, @City, @Country, @PostCode); 
            SELECT SCOPE_IDENTITY();";

        int personId;
        using (SqlCommand personCommand = new(personQueryString, connection))
        {
            personCommand.Parameters.AddWithValue("@FirstName", c.FirstName);
            personCommand.Parameters.AddWithValue("@LastName", c.LastName);
            personCommand.Parameters.AddWithValue("@Email", c.Email ?? (object)DBNull.Value);
            personCommand.Parameters.AddWithValue("@Phone", c.Phone ?? (object)DBNull.Value);
            personCommand.Parameters.AddWithValue("@Street", c.Street ?? (object)DBNull.Value);
            personCommand.Parameters.AddWithValue("@StreetNumber", c.StreetNumber ?? (object)DBNull.Value);
            personCommand.Parameters.AddWithValue("@City", c.City ?? (object)DBNull.Value);
            personCommand.Parameters.AddWithValue("@Country", c.Country ?? (object)DBNull.Value);
            personCommand.Parameters.AddWithValue("@PostCode", c.PostCode ?? (object)DBNull.Value);

            personId = Convert.ToInt32(personCommand.ExecuteScalar());
        }

        // Then insert into CustomerDatabase
        string customerQueryString = @"
            INSERT INTO CustomerDatabase (PersonId, LastPurchaseDate) 
            VALUES (@PersonId, @LastPurchaseDate); 
            SELECT SCOPE_IDENTITY();";

        using (SqlCommand customerCommand = new(customerQueryString, connection))
        {
            customerCommand.Parameters.AddWithValue("@PersonId", personId);
            customerCommand.Parameters.AddWithValue("@LastPurchaseDate", c.LastPurchaseDate ?? (object)DBNull.Value);

            c.CustomerId = Convert.ToInt32(customerCommand.ExecuteScalar());
        }
    }    public void UpdateCustomer(Customer c)
    {
        SqlConnection connection = GetConnection();

        // Get the PersonId for this customer
        string getPersonIdSql = "SELECT PersonId FROM CustomerDatabase WHERE CustomerId = @CustomerId";
        int personId;
        
        using (SqlCommand getPersonCmd = new SqlCommand(getPersonIdSql, connection))
        {
            getPersonCmd.Parameters.AddWithValue("@CustomerId", c.CustomerId);
            personId = Convert.ToInt32(getPersonCmd.ExecuteScalar());
        }

        // Update PersonDatabase
        string updatePersonSql = @"
            UPDATE PersonDatabase 
            SET FirstName = @FirstName, LastName = @LastName, Email = @Email, 
                Phone = @Phone, Street = @Street, StreetNumber = @StreetNumber, 
                City = @City, Country = @Country, PostCode = @PostCode 
            WHERE PersonId = @PersonId";
            
        using (SqlCommand updatePersonCommand = new(updatePersonSql, connection))
        {
            updatePersonCommand.Parameters.AddWithValue("@PersonId", personId);
            updatePersonCommand.Parameters.AddWithValue("@FirstName", c.FirstName);
            updatePersonCommand.Parameters.AddWithValue("@LastName", c.LastName);
            updatePersonCommand.Parameters.AddWithValue("@Email", c.Email ?? (object)DBNull.Value);
            updatePersonCommand.Parameters.AddWithValue("@Phone", c.Phone ?? (object)DBNull.Value);
            updatePersonCommand.Parameters.AddWithValue("@Street", c.Street ?? (object)DBNull.Value);
            updatePersonCommand.Parameters.AddWithValue("@StreetNumber", c.StreetNumber ?? (object)DBNull.Value);
            updatePersonCommand.Parameters.AddWithValue("@City", c.City ?? (object)DBNull.Value);
            updatePersonCommand.Parameters.AddWithValue("@Country", c.Country ?? (object)DBNull.Value);
            updatePersonCommand.Parameters.AddWithValue("@PostCode", c.PostCode ?? (object)DBNull.Value);

            updatePersonCommand.ExecuteNonQuery();
        }

        // Update CustomerDatabase
        string updateCustomerSql = @"
            UPDATE CustomerDatabase 
            SET LastPurchaseDate = @LastPurchaseDate 
            WHERE CustomerId = @CustomerId";
            
        using (SqlCommand updateCustomerCommand = new(updateCustomerSql, connection))
        {
            updateCustomerCommand.Parameters.AddWithValue("@CustomerId", c.CustomerId);
            updateCustomerCommand.Parameters.AddWithValue("@LastPurchaseDate", c.LastPurchaseDate ?? (object)DBNull.Value);

            updateCustomerCommand.ExecuteNonQuery();
        }
    }    public void DeleteCustomer(int id)
    {
        SqlConnection connection = GetConnection();

        // Get the PersonId for this customer
        string getPersonIdSql = "SELECT PersonId FROM CustomerDatabase WHERE CustomerId = @CustomerId";
        int personId;
        
        using (SqlCommand getPersonCmd = new SqlCommand(getPersonIdSql, connection))
        {
            getPersonCmd.Parameters.AddWithValue("@CustomerId", id);
            personId = Convert.ToInt32(getPersonCmd.ExecuteScalar());
        }

        // Delete from CustomerDatabase first (foreign key constraint)
        string deleteCustomerSql = "DELETE FROM CustomerDatabase WHERE CustomerId = @CustomerId";
        using (SqlCommand deleteCustomerCommand = new(deleteCustomerSql, connection))
        {
            deleteCustomerCommand.Parameters.AddWithValue("@CustomerId", id);
            deleteCustomerCommand.ExecuteNonQuery();
        }

        // Then delete from PersonDatabase
        string deletePersonSql = "DELETE FROM PersonDatabase WHERE PersonId = @PersonId";
        using (SqlCommand deletePersonCommand = new(deletePersonSql, connection))
        {
            deletePersonCommand.Parameters.AddWithValue("@PersonId", personId);
            deletePersonCommand.ExecuteNonQuery();
        }
        
        _customers.RemoveAll(c => c.CustomerId == id);
    }
}