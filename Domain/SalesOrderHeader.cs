public class SalesOrderHeader
{
    public int OrderId { get; set; }
    public string Created { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");
    public string OrderCompleted { get; set; } = "";
    public string OrderCompletedTime { get; set; } = "";
    public int CustomerId { get; set; }
    
    // Customer information for display purposes
    public string CustomerFirstName { get; set; } = "";
    public string CustomerLastName { get; set; } = "";
    public string FullName => $"{CustomerFirstName} {CustomerLastName}".Trim();
    
    private SalesOrderState _state = SalesOrderState.Pending;
    public SalesOrderState State 
    { 
        get => _state;
        set 
        {
            // If changing to Completed and wasn't completed before, set timestamp
            if (value == SalesOrderState.Completed && _state != SalesOrderState.Completed)
            {
                OrderCompleted = DateTime.Now.ToString("dd-MM-yyyy");
                OrderCompletedTime = DateTime.Now.ToString("HH:mm:ss");
            }
            // If changing away from Completed, clear the timestamp
            else if (value != SalesOrderState.Completed && _state == SalesOrderState.Completed)
            {
                OrderCompleted = "";
                OrderCompletedTime = "";
            }
            _state = value;
        }
    }
    
    public List<SalesOrderLine> OrderLines { get; set; } = new();
    
    public decimal OrderAmount => OrderLines.Sum(line => line.LineTotal);
}