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
        return _customers.ToArray();
    }

    public void AddCompany(Customer c)
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