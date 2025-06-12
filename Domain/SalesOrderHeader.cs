public class SalesOrderHeader
{
    public int OrderId { get; set; }
    public string Created { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");
    public string OrderCompleted { get; set; } = "";
    public string OrderCompletedTime { get; set; } = "";
    public int CustomerId { get; set; }
    public string State { get; set; } = "";
    public List<SalesOrderLine> OrderLines { get; set; } = new();
    
    public decimal OrderAmount => OrderLines.Sum(line => line.LineTotal);
}