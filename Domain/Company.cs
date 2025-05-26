using TECHCOOL.UI;

public class Company
{
    public int CompanyId { get; set; }
    public string Name { get; set; } = "";
    public string Street { get; set; } = "";
    public string StreetNumber { get; set; } = "";
    public string City { get; set; } = "";
    public string Country { get; set; } = "";
    public string PostCode { get; set; } = "";
    public Currency Currency { get; set; }
}