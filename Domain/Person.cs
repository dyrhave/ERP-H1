public abstract class Person
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public Address Address { get; set; } = new Address();
    public string Street { 
        get => Address.Street; 
        set => Address.Street = value; 
    }
    public string StreetNumber { 
        get => Address.StreetNumber; 
        set => Address.StreetNumber = value; 
    }
    public string City  { 
        get => Address.City; 
        set => Address.City = value; 
    }
    public string Country { 
        get => Address.Country; 
        set => Address.Country = value; 
    }
    public string PostCode { 
        get => Address.PostCode; 
        set => Address.PostCode = value; 
    }
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string LastPurchaseDate { get; set; } = "";

    // method to return the full name 
    public string FullName => $"{FirstName} {LastName}";

    /*public string CustomerFullAddress()
    {
        return $"{Street} {StreetNumber}, {City}, {PostCode}, {Country}";
    }*/
    public string FullAddress {
        get
        {
            return $"{Street} {StreetNumber}, {City}, {PostCode}, {Country}";
        }
    }
}