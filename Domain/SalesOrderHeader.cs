public class SalesOrder
{
    public int OrderId { get; set; }
    public string Created { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");

    public string OrderCompleted { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");
    public string OrderCompletedTime { get; set; } = DateTime.Now.ToString("hh:mm:ss");
    public int CustomerId { get; set; }
    public string State { get; set; } = "";
    public List<Product> OrderItems { get; set; } = new();

    public decimal TotalPrice => OrderItems.Sum(item => item.Price * item.Quantity);
    
}