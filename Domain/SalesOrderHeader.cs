using Org.BouncyCastle.Tls.Crypto.Impl.BC;

public class SalesHeader
{
    public int OrderId { get; set; }
    public string Created { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");

    public string OrderCompleted { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");
    public string OrderCompletedTime { get; set; } = DateTime.Now.ToString("hh:mm:ss");
    public int CustomerId { get; set; }
    public string State { get; set; } = "";
    public List<Product> OrderItems { get; set; } = new();

    // public decimal TotalPrice()
    // {
    //     decimal totalPrice = 0;
    //     foreach (var item in OrderItems)
    //     {
    //         totalPrice += item.Price * item.Quantity;
    //     }
    //     return totalPrice;
    // }
}