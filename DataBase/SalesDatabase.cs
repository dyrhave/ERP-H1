using Microsoft.Data.SqlClient;

public partial class Database
{
    List<SalesOrderHeader> sales = new();
    
    // S1 - Hent salgsordre ud fra id
    public SalesOrderHeader? GetSalesOrderById(int id)
    {
        foreach (var sale in sales)
        {
            if (sale.OrderId == id)
            {
                return sale;
            }
        }
        return null;
    }    
    
    public SalesOrderHeader[] GetSalesOrders()
    {
        List<SalesOrderHeader> salesList = new();
        SqlConnection connection = GetConnection();
            
        string queryString = @"
            SELECT s.OrderId, s.Created, s.OrderCompleted, s.CustomerId, s.State, s.OrderAmount,
                   c.CustomerId, c.LastPurchaseDate,
                   p.FirstName, p.LastName, p.Email, p.Phone
            FROM SalesDatabase s
            INNER JOIN CustomerDatabase c ON s.CustomerId = c.CustomerId
            INNER JOIN PersonDatabase p ON c.PersonId = p.PersonId";
            
        using (SqlCommand command = new(queryString, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {                while (reader.Read())
                {                    SalesOrderHeader sale = new()
                    {
                        OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                        Created = reader.GetDateTime(reader.GetOrdinal("Created")).ToString("dd-MM-yyyy"),
                        OrderCompleted = reader.IsDBNull(reader.GetOrdinal("OrderCompleted")) ? "" : reader.GetDateTime(reader.GetOrdinal("OrderCompleted")).ToString("dd-MM-yyyy"),
                        OrderCompletedTime = reader.IsDBNull(reader.GetOrdinal("OrderCompleted")) ? "" : reader.GetDateTime(reader.GetOrdinal("OrderCompleted")).ToString("HH:mm:ss"),
                        CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                        State = Enum.Parse<SalesOrderState>(reader.GetString(reader.GetOrdinal("State"))),
                        CustomerFirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        CustomerLastName = reader.GetString(reader.GetOrdinal("LastName")),
                        OrderLines = new List<SalesOrderLine>()
                    };
                    salesList.Add(sale);
                }
            }
        }
        return salesList.ToArray();
    }    
    public void AddSalesOrder(SalesOrderHeader sale)
    {
        SqlConnection connection = GetConnection();

        string queryString = @"
            INSERT INTO SalesDatabase (Created, OrderCompleted, CustomerId, State, OrderAmount) 
            VALUES (@Created, @OrderCompleted, @CustomerId, @State, @OrderAmount); 
            SELECT SCOPE_IDENTITY();";

        using (SqlCommand command = new(queryString, connection))
        {
            command.Parameters.AddWithValue("@Created", DateTime.Now);
            
            if (!string.IsNullOrEmpty(sale.OrderCompleted))
            {
                try
                {
                    DateTime orderCompletedDate = DateTime.ParseExact(sale.OrderCompleted, "dd-MM-yyyy", null);
                    command.Parameters.AddWithValue("@OrderCompleted", orderCompletedDate);
                }
                catch
                {
                    command.Parameters.AddWithValue("@OrderCompleted", DBNull.Value);
                }
            }
            else
            {
                command.Parameters.AddWithValue("@OrderCompleted", DBNull.Value);
            }
            command.Parameters.AddWithValue("@CustomerId", sale.CustomerId);
            command.Parameters.AddWithValue("@State", sale.State.ToString());
            command.Parameters.AddWithValue("@OrderAmount", (object?)sale.OrderAmount ?? DBNull.Value);

            try
            {
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int newOrderId))
                {
                    sale.OrderId = newOrderId;
                    sale.Created = DateTime.Now.ToString("dd-MM-yyyy");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error adding sales order: {ex.Message}");
                throw;
            }
        }
    }
    
    public void UpdateSalesOrder(SalesOrderHeader sale)
    {
        SqlConnection connection = GetConnection();
        string queryString = @"
            UPDATE SalesDatabase 
            SET OrderCompleted = @OrderCompleted, 
                CustomerId = @CustomerId, State = @State, OrderAmount = @OrderAmount 
            WHERE OrderId = @OrderId";

        using (SqlCommand command = new(queryString, connection))
        {
            command.Parameters.AddWithValue("@OrderId", sale.OrderId);
            
            if (!string.IsNullOrEmpty(sale.OrderCompleted))
            {
                try
                {
                    DateTime orderCompletedDate = DateTime.ParseExact(sale.OrderCompleted, "dd-MM-yyyy", null);
                    command.Parameters.AddWithValue("@OrderCompleted", orderCompletedDate);
                }
                catch
                {
                    command.Parameters.AddWithValue("@OrderCompleted", DBNull.Value);
                }
            }
            else
            {
                command.Parameters.AddWithValue("@OrderCompleted", DBNull.Value);
            }
            command.Parameters.AddWithValue("@CustomerId", sale.CustomerId);
            command.Parameters.AddWithValue("@State", sale.State.ToString());
            command.Parameters.AddWithValue("@OrderAmount", (object?)sale.OrderAmount ?? DBNull.Value);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error updating sales order: {ex.Message}");
                throw;
            }
        }
    }    
    
    public void DeleteSalesOrder(int id)
    {
        SqlConnection connection = GetConnection();
        
        string queryString = "DELETE FROM SalesDatabase WHERE OrderId = @OrderId";
        using (SqlCommand command = new(queryString, connection))
        {
            command.Parameters.AddWithValue("@OrderId", id);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error deleting sales order: {ex.Message}");
                throw;
            }
        }
        
        sales.RemoveAll(s => s.OrderId == id);
    }
    
    public SalesOrderHeader? GetSalesOrderWithOrderLines(int orderId)
    {
        SqlConnection connection = GetConnection();
        SalesOrderHeader? sale = null;
        
        // First get the sales order header
        string headerQuery = @"
            SELECT s.OrderId, s.Created, s.OrderCompleted, s.CustomerId, s.State, s.OrderAmount,
                   c.CustomerId, c.LastPurchaseDate,
                   p.FirstName, p.LastName, p.Email, p.Phone
            FROM SalesDatabase s
            INNER JOIN CustomerDatabase c ON s.CustomerId = c.CustomerId
            INNER JOIN PersonDatabase p ON c.PersonId = p.PersonId
            WHERE s.OrderId = @OrderId";
            
        using (SqlCommand headerCommand = new(headerQuery, connection))
        {
            headerCommand.Parameters.AddWithValue("@OrderId", orderId);
            using (SqlDataReader reader = headerCommand.ExecuteReader())
            {
                if (reader.Read())
                {                    sale = new SalesOrderHeader()
                    {
                        OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                        Created = reader.GetDateTime(reader.GetOrdinal("Created")).ToString("dd-MM-yyyy"),
                        OrderCompleted = reader.IsDBNull(reader.GetOrdinal("OrderCompleted")) ? "" : reader.GetDateTime(reader.GetOrdinal("OrderCompleted")).ToString("dd-MM-yyyy"),
                        OrderCompletedTime = reader.IsDBNull(reader.GetOrdinal("OrderCompleted")) ? "" : reader.GetDateTime(reader.GetOrdinal("OrderCompleted")).ToString("HH:mm:ss"),
                        CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                        State = Enum.Parse<SalesOrderState>(reader.GetString(reader.GetOrdinal("State"))),
                        OrderLines = new List<SalesOrderLine>()
                    };
                }
            }
        }
        
        // Then get the order lines
        if (sale != null)
        {
            string linesQuery = @"
                SELECT ol.OrderLineId, ol.OrderId, ol.ProductId, ol.Quantity, ol.UnitPrice
                FROM SalesOrderLineDatabase ol
                WHERE ol.OrderId = @OrderId";
                
            using (SqlCommand linesCommand = new(linesQuery, connection))
            {
                linesCommand.Parameters.AddWithValue("@OrderId", orderId);
                using (SqlDataReader reader = linesCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SalesOrderLine orderLine = new()
                        {
                            OrderLineId = reader.GetInt32(reader.GetOrdinal("OrderLineId")),
                            OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                            ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                            Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"))
                        };
                        sale.OrderLines.Add(orderLine);
                    }
                }
            }
        }

        return sale;
    }
    
    // Add order line to existing sales order
    public void AddSalesOrderLine(int orderId, int productId, decimal quantity, decimal unitPrice)
    {
        SqlConnection connection = GetConnection();
        
        string queryString = @"
            INSERT INTO SalesOrderLineDatabase (OrderId, ProductId, Quantity, UnitPrice) 
            VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice)";

        using (SqlCommand command = new(queryString, connection))
        {
            command.Parameters.AddWithValue("@OrderId", orderId);
            command.Parameters.AddWithValue("@ProductId", productId);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@UnitPrice", unitPrice);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error adding order line: {ex.Message}");
                throw;
            }
        }
    }
    
    public void DeleteSalesOrderLine(int orderLineId)
    {
        SqlConnection connection = GetConnection();
        
        string queryString = "DELETE FROM SalesOrderLineDatabase WHERE OrderLineId = @OrderLineId";
        using (SqlCommand command = new(queryString, connection))
        {
            command.Parameters.AddWithValue("@OrderLineId", orderLineId);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error deleting order line: {ex.Message}");
                throw;
            }
        }
    }
}