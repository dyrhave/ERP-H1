using TECHCOOL.UI;

public class Company
{
    public int CompanyId { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; } = new Address();
    public string Name { get; set; } = "";
    public string Street {
        get => Address.Street;
        set => Address.Street = value;
    }
    public string StreetNumber
    {
        get => Address.StreetNumber;
        set => Address.StreetNumber = value;
    }
    public string City
    {
        get => Address.City;
        set => Address.City = value;
    }
    public string Country
    {
        get => Address.Country;
        set => Address.Country = value;
    }
    public string PostCode
    {
        get => Address.PostCode;
        set => Address.PostCode = value;
    }
    public Currency Currency { get; set; }
}