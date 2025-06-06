public abstract class Person
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Street { get; set; } = "";
    public string StreetNumber { get; set; } = "";
    public string City { get; set; } = "";
    public string Country { get; set; } = "";
    public string PostCode { get; set; } = "";
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