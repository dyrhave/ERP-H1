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
    public SalesOrder[] GetSales()
    {
        List<SalesOrder> salesList = new();
        using (SqlConnection connection = GetConnection())
        {
            connection.Open();
            string queryString = "SELECT * FROM SalesOrders";
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
        }
        return salesList.ToArray();
    }

    public void AddSale(SalesOrder sale)
    {
        if (sale.OrderId == 0)
        {
            sales.Add(sale);
            sale.OrderId = sales.Count;
        }
    }
    public void UpdateSale(SalesOrder sale)
    {
        if (sale.OrderId == 0)
        {
            return;
        }
        SalesOrder? oldsale = GetSaleById(sale.OrderId);
        if (oldsale == null)
        {
            return;
        }
        oldsale.OrderId = sale.OrderId;
        oldsale.OrderItems = sale.OrderItems;
        oldsale.State = sale.State;
        //sprøg simmon omkring S1 update
        
    }
    public void DeleteSale(int id)
    {
        SalesOrder? sale = GetSaleById(id);
        if (sale != null)
        sales.Remove(sale);
    }
}