using Microsoft.Data.SqlClient;

public partial class Database
{
    List<Product> products = new();
    public Product? GetProductById(int id)
    {
        SqlConnection conn = GetConnection();

        string queryString = @"
            SELECT p.ProductId, p.Name, p.Description, p.Price, p.BuyInPrice, 
                   p.Quantity, p.Location, p.Unit
            FROM ProductDatabase p
            WHERE p.ProductId = @ProductId";

        using (SqlCommand command = new(queryString, conn))
        {
            command.Parameters.AddWithValue("@ProductId", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Product()
                    {
                        ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description")),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                        BuyInPrice = reader.GetDecimal(reader.GetOrdinal("BuyInPrice")),
                        Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                        Location = reader.IsDBNull(reader.GetOrdinal("Location")) ? string.Empty : reader.GetString(reader.GetOrdinal("Location")),
                        Unit = reader.IsDBNull(reader.GetOrdinal("Unit")) ? string.Empty : reader.GetString(reader.GetOrdinal("Unit"))
                    };
                }
            }
        }

        return null;
    }
    public Product[] GetProduct()
    {
        List<Product> productsList = new();
        SqlConnection conn = GetConnection();
        
        string queryString = @"
            SELECT p.ProductId, p.Name, p.Description, p.Price, p.BuyInPrice, 
                   p.Quantity, p.Location, p.Unit
            FROM ProductDatabase p";
            
        using (SqlCommand command = new(queryString, conn))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new()
                    {
                        ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description")),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                        BuyInPrice = reader.GetDecimal(reader.GetOrdinal("BuyInPrice")),
                        Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                        Location = reader.IsDBNull(reader.GetOrdinal("Location")) ? string.Empty : reader.GetString(reader.GetOrdinal("Location")),
                        Unit = reader.IsDBNull(reader.GetOrdinal("Unit")) ? string.Empty : reader.GetString(reader.GetOrdinal("Unit"))
                    };
                    productsList.Add(product);
                }
            }
        }
        
        return productsList.ToArray();
    }    
    public void AddProduct(Product product)
    {
        SqlConnection conn = GetConnection();

        string sql = @"
            INSERT INTO ProductDatabase (Name, Description, Price, BuyInPrice, Quantity, Location, Unit)
            VALUES (@Name, @Description, @Price, @BuyInPrice, @Quantity, @Location, @Unit);
            SELECT SCOPE_IDENTITY();";

        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Name", product.Name);        cmd.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Price", product.Price);
        cmd.Parameters.AddWithValue("@BuyInPrice", product.BuyInPrice);
        cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
        cmd.Parameters.AddWithValue("@Location", product.Location ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Unit", product.Unit ?? (object)DBNull.Value);

        product.ProductId = Convert.ToInt32(cmd.ExecuteScalar());
    }    
    public void UpdateProduct(Product product)
    {
        SqlConnection conn = GetConnection();

        string sql = @"
            UPDATE ProductDatabase
            SET Name = @Name, Description = @Description, Price = @Price, 
                BuyInPrice = @BuyInPrice, Quantity = @Quantity, Location = @Location, Unit = @Unit
            WHERE ProductId = @ProductId";

        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ProductId", product.ProductId);        cmd.Parameters.AddWithValue("@Name", product.Name);
        cmd.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Price", product.Price);
        cmd.Parameters.AddWithValue("@BuyInPrice", product.BuyInPrice);
        cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
        cmd.Parameters.AddWithValue("@Location", product.Location ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@Unit", product.Unit ?? (object)DBNull.Value);

        cmd.ExecuteNonQuery();
    }    
    public void DeleteProduct(int id)
    {
        SqlConnection conn = GetConnection();

        string sql = "DELETE FROM ProductDatabase WHERE ProductId = @ProductId";
        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ProductId", id);

        cmd.ExecuteNonQuery();

        products.RemoveAll(p => p.ProductId == id);
    }

    public Product? GetProductByIdWithDetails(int id)
    {
        SqlConnection conn = GetConnection();
        
        string queryString = @"
            SELECT p.ProductId, p.Name, p.Description, p.Price, p.BuyInPrice, 
                   p.Quantity, p.Location, p.Unit
            FROM ProductDatabase p
            WHERE p.ProductId = @ProductId";
            
        using (SqlCommand command = new(queryString, conn))
        {
            command.Parameters.AddWithValue("@ProductId", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Product()
                    {
                        ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description")),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                        BuyInPrice = reader.GetDecimal(reader.GetOrdinal("BuyInPrice")),
                        Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                        Location = reader.IsDBNull(reader.GetOrdinal("Location")) ? string.Empty : reader.GetString(reader.GetOrdinal("Location")),
                        Unit = reader.IsDBNull(reader.GetOrdinal("Unit")) ? string.Empty : reader.GetString(reader.GetOrdinal("Unit"))
                    };
                }
            }
        }
        return null;
    }
}