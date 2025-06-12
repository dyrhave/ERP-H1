public class SalesOrderLine
{
    public int OrderLineId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    
    public decimal LineTotal => Quantity * UnitPrice;
}
