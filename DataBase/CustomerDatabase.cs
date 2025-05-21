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

    public Customer[] GetCustomers() // Temporary structure - fix when database is implemented
    {
        List<Customer> customerList = new();
        using (SqlConnection connection = new())
        {
            connection.Open();
            string queryString = "SELECT * FROM Customers";
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
                            Street = reader.GetString(5),
                            StreetNumber = reader.GetString(6),
                            City = reader.GetString(7),
                            Country = reader.GetString(8),
                            PostCode = reader.GetString(9)
                        };
                        customerList.Add(customer);
                    }
                }
            }
        }
        return customerList.ToArray();
    }

    public void AddCustomer(Customer c)
    {
        if (c.CustomerId == 0)
        {
            _customers.Add(c);
            c.CustomerId = _customers.Count;
        }
    }

    public void UpdateCustomer(Customer c)
    {
        if (c.CustomerId == 0)
        {
            return;
        }
        
        Customer? oldCustomer = GetCustomerById(c.CustomerId);
        if (oldCustomer == null)
        {
            return;
        }
        oldCustomer.FirstName = c.FirstName;
        oldCustomer.LastName = c.LastName;
        oldCustomer.Email = c.Email;
        oldCustomer.Phone = c.Phone;
        oldCustomer.Street = c.Street;
        oldCustomer.StreetNumber = c.StreetNumber;
        oldCustomer.City = c.City;
        oldCustomer.Country = c.Country;
        oldCustomer.PostCode = c.PostCode;
    }

    public void DeleteCustomer(int id)
    {
        Customer? customer = GetCustomerById(id);
        if (customer != null)
        {
            _customers.Remove(customer);
        }
    }
}