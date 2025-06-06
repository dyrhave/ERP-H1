using Microsoft.Data.SqlClient;

public partial class Database
{
    List<SalesOrder> sales = new();
    public SalesOrder? GetSaleById(int id)
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
    public SalesOrder[] GetSales() // Temporary structure - fix when database is implemented
    {
        List<SalesOrder> salesList = new();
        SqlConnection connection = GetConnection();
            
        string queryString = "SELECT * FROM SalesDatabase";
        using (SqlCommand command = new(queryString, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    SalesOrder sale = new()
                    {
                        OrderId = reader.GetInt32(0),
                        Created = reader.GetString(1),
                        OrderCompleted = reader.GetString(2),
                        OrderCompletedTime = reader.GetString(3),
                        CustomerId = reader.GetInt32(4),
                        State = reader.GetString(5),
                        OrderItems = new List<Product>()
                    };
                    salesList.Add(sale);
                }
            }
        }
    return salesList.ToArray();
    }

    public void AddSale(SalesOrder sale)
    {
        SqlConnection connection = GetConnection();

        string queryString = "INSERT INTO SalesDatabase (Created, OrderCompleted, OrderCompletedTime, CustomerId, State) " +
                             "VALUES (@Created, @OrderCompleted, @OrderCompletedTime, @CustomerId, @State); " +
                             "SELECT SCOPE_IDENTITY();";

        using (SqlCommand command = new(queryString, connection))
        {
            command.Parameters.AddWithValue("@Created", sale.Created);
            command.Parameters.AddWithValue("@OrderCompleted", sale.OrderCompleted);
            command.Parameters.AddWithValue("@OrderCompletedTime", sale.OrderCompletedTime);
            command.Parameters.AddWithValue("@CustomerId", sale.CustomerId);
            command.Parameters.AddWithValue("@State", sale.State);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error adding sale: {ex.Message}");
                throw;
            }
        }
    }
    public void UpdateSale(SalesOrder sale)
    {
        SqlConnection connection = GetConnection();

        string queryString = "UPDATE SalesDatabase SET Created = @Created, OrderCompleted = @OrderCompleted, " +
                             "OrderCompletedTime = @OrderCompletedTime, CustomerId = @CustomerId, State = @State " +
                             "WHERE OrderId = @OrderId;";

        using (SqlCommand command = new(queryString, connection))
        {
            command.Parameters.AddWithValue("@OrderId", sale.OrderId);
            command.Parameters.AddWithValue("@Created", sale.Created);
            command.Parameters.AddWithValue("@OrderCompleted", sale.OrderCompleted);
            command.Parameters.AddWithValue("@OrderCompletedTime", sale.OrderCompletedTime);
            command.Parameters.AddWithValue("@CustomerId", sale.CustomerId);
            command.Parameters.AddWithValue("@State", sale.State);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error updating sale: {ex.Message}");
                throw;
            }
        }
        
    }
    public void DeleteSale(int id)
    {
        SqlConnection connection = GetConnection();
        string queryString = "DELETE FROM SalesDatabase WHERE OrderId = @OrderId;";
        using (SqlCommand command = new(queryString, connection))
        {
            command.Parameters.AddWithValue("@OrderId", id);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error deleting sale: {ex.Message}");
                throw;
            }
        }
    }
}