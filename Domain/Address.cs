public class Address
{
    public string Street { get; set; } = "";
    public string StreetNumber { get; set; } = "";
    public string City { get; set; } = "";
    public string Country { get; set; } = "";
    public string PostCode { get; set; } = "";
    public string GetFullAddress()
    {
        return $"{Street} {StreetNumber}, {City}, {Country}, {PostCode}";
    } 
}