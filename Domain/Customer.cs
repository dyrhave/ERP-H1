public class Customer : Person
{
    public int CustomerId { get; set; }
    public new string LastPurchaseDate { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");
}